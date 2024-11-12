using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
{
    public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .OrderBy(o => o.OrderName.Value)
            .ApplyPagination(query.Pagination)
            .ToListAsync(cancellationToken: cancellationToken);
        return orders.ToGetOrdersQueryResult(query.Pagination);
    }
}