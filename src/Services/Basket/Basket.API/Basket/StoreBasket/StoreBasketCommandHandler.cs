using FluentValidation;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(
    string UserName
    ) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
    }
}

public class StoreBasketCommandHandler
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new StoreBasketResult(request.UserName));
        // TODO: Implement the command handler
    }
}