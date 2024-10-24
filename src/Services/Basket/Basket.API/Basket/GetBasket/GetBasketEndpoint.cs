namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(
    string UserName,
    List<BasketItemResponse> Items,
    decimal TotalPrice
);

public record BasketItemResponse(
    uint Quantity,
    string ProductName,
    decimal Price,
    string Color,
    string ImageUrl
);

public sealed class GetBasketEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender, IMapper mapper) =>
            {
                var query = new BasketQuery(userName);
                var result = await sender.Send(query);
                var response = mapper.Map<GetBasketResponse>(result);
                return Results.Ok(response);
            })
            .Produces<GetBasketResponse>(StatusCodes.Status200OK);

    }
}