using Basket.API.Dtos;
using Carter.OpenApi;
using Mapster;

namespace Basket.API.Basket.CheckoutBasket;
public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto) : IRequest<BasketCheckoutDto>;
public record BasketCheckoutResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<BasketCheckoutResponse>();
                return response;
            })
            .Produces<BasketCheckoutDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("CheckoutBasket")
            .IncludeInOpenApi()
            .WithDescription("This endpoint checks out a basket.");
    }
}