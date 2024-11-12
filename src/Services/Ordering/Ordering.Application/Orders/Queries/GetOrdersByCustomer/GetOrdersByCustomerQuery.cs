namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetQueryByCustomerResult>;

public record GetQueryByCustomerResult(IEnumerable<OrderDto> Orders);

public class GetQueryByCustomerQueryValidator : AbstractValidator<GetOrdersByCustomerQuery>
{
    public GetQueryByCustomerQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}