using Basket.API.Data;

namespace Basket.API.Basket.GetBasket;
public record BasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);
public class GetBasketQueryHandler(IBasketRepository repository, IMapper mapper)
    : IQueryHandler<BasketQuery, GetBasketResult>
{

    public async Task<GetBasketResult> Handle(BasketQuery request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasketAsync(request.UserName, cancellationToken);
        return mapper.Map<GetBasketResult>(cart);
    }
}