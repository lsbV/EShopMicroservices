namespace Catalog.API.Products.GetProducts;

public record PaginationInfo(int PageNumber, int PageSize);

public record GetProductsRequest(int? PageNumber, int? PageSize);

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, IMapper mapper, [AsParameters] GetProductsRequest request) =>
            {
                var paginationInfo = new PaginationInfo(request.PageNumber ?? 1, request.PageSize ?? 4);
                var result = await sender.Send(new GetProductsQuery(paginationInfo));

                var response = mapper.Map<GetProductsResponse>(result);

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .WithDescription("Gets all products")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK);
    }
}