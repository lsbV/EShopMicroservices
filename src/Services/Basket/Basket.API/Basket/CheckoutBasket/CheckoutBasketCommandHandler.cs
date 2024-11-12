using Basket.API.Data;
using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using FluentValidation;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto)
    : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull();
        RuleFor(x => x.BasketCheckoutDto.City).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.Country).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.CustomerId).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.FirstName).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.LastName).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.State).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.Street).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.ZipCode).NotEmpty();

        RuleFor(x => x.BasketCheckoutDto.CardExpiration).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.CardHolderName).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.CardNumber).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.Cvv).NotEmpty();
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint endpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);

        var eventMessage = new BasketCheckoutEvent(
            command.BasketCheckoutDto.UserName,
            command.BasketCheckoutDto.CustomerId,
            command.BasketCheckoutDto.FirstName,
            command.BasketCheckoutDto.LastName,
            command.BasketCheckoutDto.City,
            command.BasketCheckoutDto.Street,
            command.BasketCheckoutDto.State,
            command.BasketCheckoutDto.Country,
            command.BasketCheckoutDto.ZipCode,
            command.BasketCheckoutDto.CardNumber,
            command.BasketCheckoutDto.CardHolderName,
            command.BasketCheckoutDto.CardExpiration,
            command.BasketCheckoutDto.Cvv,
            command.BasketCheckoutDto.PaymentMethod,
            basket.TotalPrice
        );

        await endpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}