using Basket.API.Data;
using Basket.API.Infrastructure.Exceptions;
using Discount.Grpc;
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

public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        await ApplyDiscount(request.Cart, cancellationToken);
        await repository.UpdateBasketAsync(request.Cart, cancellationToken);
        return new StoreBasketResult(request.Cart.UserName);
    }

    private async Task ApplyDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var discount = await discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= discount.Amount;
        }
    }

}