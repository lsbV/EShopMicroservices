namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);

public sealed class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender, IMapper mapper) =>
        {
            var command = mapper.Map<StoreBasketCommand>(request);

            var result = await sender.Send(command);

            var response = mapper.Map<StoreBasketResponse>(result);

            return Results.Created($"/basket/{response.UserName}", response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
        .WithSummary("Stores a basket for a user.")
        .WithDescription("Stores a basket for a user.");
    }
}