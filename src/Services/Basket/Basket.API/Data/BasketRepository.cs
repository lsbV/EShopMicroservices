using Basket.API.Infrastructure.Exceptions;

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken)
    {
        var basket = await session.Query<ShoppingCart>().FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
        return basket ?? throw new BasketNotFoundException(userName);
    }

    public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket, CancellationToken cancellationToken)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
    {
        var basket = await session.Query<ShoppingCart>().FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
        if (basket == null)
        {
            return false;
        }
        session.Delete(basket);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}