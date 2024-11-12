using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest Pagination) : IQuery<GetOrdersQueryResult>;

public record GetOrdersQueryResult(PaginationResult<OrderDto> Orders);

public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x.Pagination).NotNull();
        RuleFor(x => x.Pagination.PageSize).GreaterThan(10).LessThanOrEqualTo(100).WithMessage("PageSize must be between 10 and 100");
        RuleFor(x => x.Pagination.PageIndex).GreaterThanOrEqualTo(0).WithMessage("PageIndex must be greater than or equal to 0");

    }
}