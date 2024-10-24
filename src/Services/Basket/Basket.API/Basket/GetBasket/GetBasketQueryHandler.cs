namespace Basket.API.Basket.GetBasket;
public record BasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);
public class GetBasketQueryHandler() : IQueryHandler<BasketQuery, GetBasketResult>
{

    public Task<GetBasketResult> Handle(BasketQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetBasketResult(new ShoppingCart(request.UserName)));
        // TODO: Implement the query handler
    }
}