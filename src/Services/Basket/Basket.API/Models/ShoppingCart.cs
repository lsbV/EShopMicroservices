namespace Basket.API.Models;

public class ShoppingCart(string userName)
{
    public string UserName { get; set; } = userName;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(x => x.Quantity * x.Price);
        }
    }
}