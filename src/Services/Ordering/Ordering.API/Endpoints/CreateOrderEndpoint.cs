using Carter.OpenApi;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);

public record CreateOrderResponse(Guid OrderId);

public sealed class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var command = new CreateOrderCommand(request.Order);

                var result = await sender.Send(command);

                return result.Adapt<CreateOrderResponse>();
            })
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName("CreateOrder")
            .IncludeInOpenApi()
            .WithDescription("This endpoint creates an order.");


    }
}

