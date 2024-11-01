namespace Basket.API.Models;

public class ShoppingCartItem
{
    public required uint Quantity { get; set; }
    public required string Color { get; set; }
    public required decimal Price { get; set; }
    public Guid ProductId { get; set; }
    public required string ProductName { get; set; }
}