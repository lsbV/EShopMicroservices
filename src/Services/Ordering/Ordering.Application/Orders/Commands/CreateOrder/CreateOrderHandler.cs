namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext context)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        context.Orders.Add(order);

        await context.SaveChangesAsync(cancellationToken);


        return new CreateOrderResult(order.Id.Value);
    }

    private static Order CreateNewOrder(OrderDto orderDto)
    {
        var order = Order.Create(
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(orderDto.CustomerId),
            OrderName.Of(orderDto.OrderName),
            Address.Of(
                orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.Street,
                orderDto.ShippingAddress.City,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.ZipCode
                ),
            Address.Of(
                orderDto.BillingAddress.FirstName,
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.Street,
                orderDto.BillingAddress.City,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.ZipCode
                ),
            OrderStatus.Draft,
            Payment.Of(orderDto.Payment.CardNumber, orderDto.Payment.CardHolderName, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
            );

        foreach (var item in orderDto.Items)
        {
            order.AddOrderItem(ProductId.Of(item.ProductId), item.Price, item.Quantity);
        }
        return order;
    }
}