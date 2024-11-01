using Basket.API.Data;
using Basket.API.Infrastructure.Exceptions;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(
    ShoppingCart Cart
    ) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart is required.");
        RuleFor(x => x.Cart.UserName).NotNull().NotEmpty().WithMessage("UserName is required.");
        RuleFor(x => x.Cart.Items).NotNull().NotEmpty().WithMessage("Cart should contain at least one item.");
    }
}

public class StoreBasketCommandHandler(IBasketRepository repository, IMapper mapper)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await repository.GetBasketAsync(request.Cart.UserName, cancellationToken);
            cart.Items = request.Cart.Items;
            await repository.UpdateBasketAsync(cart, cancellationToken);
            return new StoreBasketResult(request.Cart.UserName);
        }
        catch (BasketNotFoundException)
        {
            await repository.UpdateBasketAsync(request.Cart, cancellationToken);
            return new StoreBasketResult(request.Cart.UserName);
        }
    }
}