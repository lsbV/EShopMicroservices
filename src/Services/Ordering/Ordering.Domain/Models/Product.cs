namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    protected Product(string name, decimal price) : base(ProductId.Of(Guid.NewGuid()))
    {
        Name = name;
        Price = price;
    }
    public static Product Create(ProductId id, string name, decimal price)
    {
        var product = new Product(name, price)
        {
            Id = id
        };
        return product;
    }
}