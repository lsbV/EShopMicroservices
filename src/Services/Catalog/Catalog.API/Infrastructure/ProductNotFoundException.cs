using BuildingBlocks.Exceptions;

namespace Catalog.API.Infrastructure;

internal class ProductNotFoundException(Guid id) : NotFoundException(nameof(Product), id)
{
    public Guid Id { get; } = id;

}