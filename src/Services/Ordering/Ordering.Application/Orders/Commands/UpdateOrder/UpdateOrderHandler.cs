namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await context.Orders.FindAsync([OrderId.Of(command.Order.Id)], cancellationToken: cancellationToken)
            ?? throw new OrderNotFoundException(command.Order.Id);

        order.UpdateOrderFromOrderDto(command.Order);

        await context.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }
}

file static class OrderExtensions
{
    public static void UpdateOrderFromOrderDto(this Order order, OrderDto dto)
    {
        order.Update(
            OrderName.Of(dto.OrderName),
            Address.Of(
                dto.ShippingAddress.FirstName,
                dto.ShippingAddress.LastName,
                dto.ShippingAddress.Street,
                dto.ShippingAddress.City,
                dto.ShippingAddress.State,
                dto.ShippingAddress.Country,
                dto.ShippingAddress.ZipCode
            ),
            Address.Of(
                dto.BillingAddress.FirstName,
                dto.BillingAddress.LastName,
                dto.BillingAddress.Street,
                dto.BillingAddress.City,
                dto.BillingAddress.State,
                dto.BillingAddress.Country,
                dto.BillingAddress.ZipCode
            ),
            dto.Status,
            Payment.Of(dto.Payment.CardNumber,
                dto.Payment.CardHolderName,
                dto.Payment.Expiration,
                dto.Payment.Cvv,
                dto.Payment.PaymentMethod)
        );
        order.RemoveAllOrderItems();
        foreach (var item in dto.Items)
        {
            order.AddOrderItem(ProductId.Of(item.ProductId), item.Price, item.Quantity);
        }
    }
}