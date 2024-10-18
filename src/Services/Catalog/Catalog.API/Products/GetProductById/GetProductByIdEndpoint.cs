using MediatR;
using System;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(Product Product);



public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:Guid}", async (Guid id, ISender sender, IMapper mapper) =>
            {

                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = mapper.Map<GetProductByIdResponse>(result);

                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .WithDescription("Gets a product by its id")
            .Produces<GetProductByIdResult>(StatusCodes.Status200OK);
    }
}