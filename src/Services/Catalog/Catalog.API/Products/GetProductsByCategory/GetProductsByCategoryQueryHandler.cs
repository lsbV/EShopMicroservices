using System.Linq;

namespace Catalog.API.Products.GetProductsByCategory;

// query and result models
public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);
public class GetProductsByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().Where(p => p.Categories.Contains(query.Category)).ToListAsync(cancellationToken);
        return new GetProductsByCategoryResult(products);
    }
}