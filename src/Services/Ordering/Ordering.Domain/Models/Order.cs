namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; }
    public OrderName OrderName { get; set; }
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public Payment Payment { get; private set; }
    public decimal TotalPrice => _orderItems.Sum(item => item.Price * item.Quantity);

    protected Order() : base(OrderId.Of(Guid.NewGuid()))
    {
        CustomerId = null!;
        OrderName = null!;
        ShippingAddress = null!;
        BillingAddress = null!;
        OrderStatus = default;
        Payment = null!;
    }

    protected Order(CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, OrderStatus orderStatus, Payment payment) : base(OrderId.Of(Guid.NewGuid()))
    {
        CustomerId = customerId;
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        OrderStatus = orderStatus;
        Payment = payment;
    }

    public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, OrderStatus orderStatus, Payment payment)
    {
        var order = new Order(customerId, orderName, shippingAddress, billingAddress, orderStatus, payment)
        {
            Id = id
        };
        order.AddDomainEvent(new OrderCreatedDomainEvent(order));
        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, OrderStatus orderStatus, Payment payment)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        OrderStatus = orderStatus;
        Payment = payment;
    }

    public void AddOrderItem(ProductId productId, decimal price, int quantity)
    {
        var existingOrderForProduct = _orderItems.Find(o => o.ProductId == productId);
        if (existingOrderForProduct != null)
        {
            existingOrderForProduct.AddUnits(quantity);
        }
        else
        {
            _orderItems.Add(new OrderItem(productId, Id, price, quantity));
        }
    }

    public void RemoveOrderItem(ProductId productId)
    {
        var orderItem = _orderItems.Find(o => o.ProductId == productId);
        if (orderItem != null)
        {
            _orderItems.Remove(orderItem);
        }
    }

    public void RemoveAllOrderItems()
    {
        _orderItems.Clear();
    }
}