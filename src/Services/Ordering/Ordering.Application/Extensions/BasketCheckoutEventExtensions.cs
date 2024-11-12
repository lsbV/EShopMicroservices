using BuildingBlocks.Messaging.Events;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Extensions;

public static class BasketCheckoutEventExtensions
{
    public static CreateOrderCommand ToCreateOrderCommand(this BasketCheckoutEvent basketCheckoutEvent)
    {
        var shippingAddressDto = new AddressDto(
            FirstName: basketCheckoutEvent.FirstName,
            LastName: basketCheckoutEvent.LastName,
            Street: basketCheckoutEvent.Street,
            City: basketCheckoutEvent.City,
            State: basketCheckoutEvent.State,
            Country: basketCheckoutEvent.Country,
            ZipCode: basketCheckoutEvent.ZipCode
        );
        var billingAddressDto = new AddressDto(
            FirstName: basketCheckoutEvent.FirstName,
            LastName: basketCheckoutEvent.LastName,
            Street: basketCheckoutEvent.Street,
            City: basketCheckoutEvent.City,
            State: basketCheckoutEvent.State,
            Country: basketCheckoutEvent.Country,
            ZipCode: basketCheckoutEvent.ZipCode
        );
        var paymentDto = new PaymentDto(
            CardNumber: basketCheckoutEvent.CardNumber,
            CardHolderName: basketCheckoutEvent.CardHolderName,
            Expiration: basketCheckoutEvent.CardExpiration,
            Cvv: basketCheckoutEvent.Cvv,
            PaymentMethod: basketCheckoutEvent.PaymentMethod
        );
        var orderId = Guid.NewGuid();
        var orderDto = new OrderDto(
            orderId,
            basketCheckoutEvent.CustomerId,
            basketCheckoutEvent.UserName,
            shippingAddressDto,
            billingAddressDto,
            paymentDto,
            OrderStatus.Pending,
            [
                new OrderItemDto(orderId, Guid.Parse("2BACA16F-2320-4D06-AF30-3507C94F93F7"), 100, 1),
                new OrderItemDto(orderId, Guid.Parse("16626F49-0CE0-4820-A92B-C43ABCD4B35E"), 200, 2),
            ]
        );

        return new CreateOrderCommand(orderDto);

    }
}