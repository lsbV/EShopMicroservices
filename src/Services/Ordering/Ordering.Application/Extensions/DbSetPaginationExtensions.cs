using BuildingBlocks.Pagination;

namespace Ordering.Application.Extensions;

public static class DbSetPaginationExtensions
{
    public static IQueryable<Order> ApplyPagination(this IQueryable<Order> query, PaginationRequest request)
    {
        return query
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize);
    }
}