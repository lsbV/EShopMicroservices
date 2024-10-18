namespace Catalog.API.Models;

public record Product(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price);