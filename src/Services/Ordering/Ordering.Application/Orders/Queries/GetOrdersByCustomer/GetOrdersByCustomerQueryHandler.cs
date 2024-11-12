using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrdersByCustomerQuery, GetQueryByCustomerResult>
{
    public async Task<GetQueryByCustomerResult> Handle(GetOrdersByCustomerQuery ordersByCustomerQuery, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Of(ordersByCustomerQuery.CustomerId))
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken: cancellationToken);

        return orders.ToGetQueryByCustomerResult();
    }
}