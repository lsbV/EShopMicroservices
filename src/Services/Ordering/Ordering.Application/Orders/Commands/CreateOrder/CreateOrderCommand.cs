namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid OrderId);

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(order => order.Order).NotEmpty().SetValidator(new OrderDtoValidator());
        RuleFor(o => o.Order.Id).Empty();
        RuleFor(order => order.Order.Items).ForEach(item => item.SetValidator(new OrderItemDtoValidator()));
    }
}