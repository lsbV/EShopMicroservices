namespace Basket.API.Data;

public interface IBasketRepository
{
    public Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken);
    public Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket, CancellationToken cancellationToken);
    public Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken);
}