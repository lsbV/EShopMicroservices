using Carter.OpenApi;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool IsSucceed);

public sealed class UpdateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();

                var result = await sender.Send(command);

                return result.Adapt<UpdateOrderResponse>();
            })
            .Produces<UpdateOrderResponse>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("UpdateOrder")
            .IncludeInOpenApi()
            .WithDescription("This endpoint updates an order.");
    }
}