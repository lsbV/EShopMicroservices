namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; }
    public OrderId OrderId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(ProductId productId, OrderId orderId, decimal price, int quantity) : base(OrderItemId.Of(Guid.NewGuid()))
    {
        ProductId = productId;
        OrderId = orderId;
        Price = price;
        Quantity = quantity;
    }

    internal void AddUnits(int quantity)
    {
        Quantity += quantity;
    }
}