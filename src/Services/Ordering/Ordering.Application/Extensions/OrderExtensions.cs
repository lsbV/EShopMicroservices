using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;
using Ordering.Application.Orders.Queries.GetOrdersByName;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.Application.Extensions;

internal static class OrderExtensions
{
    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto(
            order.Id.Value,
            order.CustomerId.Value,
            order.OrderName.Value,
            order.ShippingAddress.ToAddressDto(),
            order.BillingAddress.ToAddressDto(),
            order.Payment.ToPaymentDto(),
            order.OrderStatus,
            order.OrderItems.Select(oi => oi.ToOrderItemDto()).ToList()
        );
    }

    public static GetQueryByCustomerResult ToGetQueryByCustomerResult(this IEnumerable<Order> orders)
    {
        var ordersDtos = orders.Select(o => o.ToOrderDto());
        return new GetQueryByCustomerResult(ordersDtos);
    }

    public static GetOrdersQueryResult ToGetOrdersQueryResult(this IEnumerable<Order> orders, PaginationRequest request)
    {
        var ordersDtos = orders.Select(o => o.ToOrderDto()).ToList();
        return new GetOrdersQueryResult(
            new PaginationResult<OrderDto>(request.PageIndex, request.PageSize, ordersDtos.Count, ordersDtos));
    }
    public static GetOrdersByNameResult ToGetOrdersByNameResult(this IEnumerable<Order> orders)
    {
        var ordersDtos = orders.Select(o => o.ToOrderDto());
        return new GetOrdersByNameResult(ordersDtos);
    }

    public static OrderItemDto ToOrderItemDto(this OrderItem orderItem)
    {
        return new OrderItemDto(
            orderItem.Id.Value,
            orderItem.ProductId.Value,
            orderItem.Price,
            orderItem.Quantity
        );
    }

    private static PaymentDto ToPaymentDto(this Payment payment)
    {
        return new PaymentDto(
            payment.CardNumber,
            payment.CardHolderName,
            payment.Expiration,
            payment.SecurityNumber,
            payment.PaymentMethod
        );
    }

    private static AddressDto ToAddressDto(this Address address)
    {
        return new AddressDto(
            address.FirstName,
            address.LastName,
            address.Street,
            address.City,
            address.State,
            address.Country,
            address.ZipCode
        );
    }
}