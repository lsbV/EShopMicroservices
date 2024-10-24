using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.GetProductsByCategory;

namespace Catalog.API.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ConstructUsing(x => new CreateProductCommand(
                new Product(
                    Guid.Empty,
                    x.Name,
                    x.Categories,
                    x.Description,
                    x.ImageFile,
                    x.Price
                )));

        CreateMap<CreateProductResult, CreateProductResponse>();

        CreateMap<GetProductsResult, GetProductsResponse>();

        CreateMap<GetProductByIdResult, GetProductByIdResponse>();

        CreateMap<GetProductsByCategoryResult, GetProductsByCategoryResponse>();
    }
}