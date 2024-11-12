namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSucceed);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(order => order.Order).NotEmpty().SetValidator(new OrderDtoValidator());
        RuleFor(order => order.Order.Id).NotEmpty();
        RuleFor(order => order.Order.Items).ForEach(item =>
            item.SetValidator(new UpdateOrderItemValidator()));
    }
}
public class UpdateOrderItemValidator : OrderItemDtoValidator
{
    public UpdateOrderItemValidator()
    {
        RuleFor(item => item.ProductId).NotEmpty();
    }
}