using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool IsDeleted);

public sealed class DeleteOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(id));

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        })
        .Produces<DeleteOrderResponse>()
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithName("DeleteOrder")
        .IncludeInOpenApi()
        .WithDescription("This endpoint deletes an order.");

    }
}