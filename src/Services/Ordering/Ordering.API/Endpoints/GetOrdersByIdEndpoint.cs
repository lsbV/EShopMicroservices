using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{id}", async (string id, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByNameQuery(id));

            var response = result.Adapt<GetOrdersByNameResponse>();

            return Results.Ok(response);
        })
        .Produces<GetOrdersByNameResponse>()
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithName("GetOrdersByName")
        .IncludeInOpenApi()
        .WithDescription("This endpoint retrieves an order by its Name.");

    }
}