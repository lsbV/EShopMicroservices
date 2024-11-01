using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache, ILogger<CachedBasketRepository> logger)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken)
    {
        var basket = await cache.GetAsync<ShoppingCart>(userName, cancellationToken);
        if (basket is not null) return basket;
        logger.LogInformation("Basket for {UserName} was not found in cache. Fetching from repository.", userName);
        basket = await repository.GetBasketAsync(userName, cancellationToken);
        await cache.SetAsync(userName, basket, cancellationToken);
        return basket;

    }

    public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket, CancellationToken cancellationToken)
    {
        await cache.SetAsync(basket.UserName, basket, cancellationToken);
        logger.LogInformation("Basket for {UserName} was updated in cache.", basket.UserName);
        return await repository.UpdateBasketAsync(basket, cancellationToken);
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
    {
        await cache.RemoveAsync(userName, cancellationToken);
        logger.LogInformation("Basket for {UserName} was removed from cache.", userName);
        return await repository.DeleteBasketAsync(userName, cancellationToken);
    }
}