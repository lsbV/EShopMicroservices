namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:Guid}", async (Guid id, UpdateProductRequest request, ISender sender) =>
            {
                var product = new Product(id, request.Name, request.Categories, request.Description, request.ImageFile, request.Price);
                await sender.Send(new UpdateProductCommand(product));
                return Results.NoContent();
            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .WithSummary("Updates a product")
            .WithDescription("Updates a product in the catalog");

    }
}