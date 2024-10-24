namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResponse(bool IsDeleted);
public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender, IMapper mapper) =>
        {
            var command = new DeleteBasketCommand(userName);

            var result = await sender.Send(command);

            var response = mapper.Map<DeleteBasketResponse>(result);

            return Results.Ok(response);
        })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Basket is deleted")
            .WithDescription("Deletes a basket for a user.");
    }
}