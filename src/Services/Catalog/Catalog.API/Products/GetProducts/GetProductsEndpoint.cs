namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, IMapper mapper) =>
            {
                var result = await sender.Send(new GetProductsQuery());

                var response = mapper.Map<GetProductsResponse>(result);

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .WithDescription("Gets all products")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK);
    }
}