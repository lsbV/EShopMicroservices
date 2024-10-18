namespace Catalog.API.Products.GetProductsByCategory;

// request and response models
public record GetProductsByCategoryRequest(string Category);
public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule

{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender, IMapper mapper) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(category));
                var response = mapper.Map<GetProductsByCategoryResponse>(result);
                return Results.Ok(response);
            })
            .WithName("GetProductsByCategory")
            .WithDescription("Gets all products by category")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK);
    }
}