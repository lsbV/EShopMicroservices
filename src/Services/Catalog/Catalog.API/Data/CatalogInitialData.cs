using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(token: cancellation))
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static List<Product> GetPreconfiguredProducts()
    {
        List<Product> products =
        [
            new Product( Guid.NewGuid(), "Keyboard",  ["Electronics"] , "Ergonomic keyboard", "keyboard.jpg", 20),
            new Product( Guid.NewGuid(), "Mouse", ["Electronics"], "Wireless mouse", "mouse.jpg", 12),
            new Product( Guid.NewGuid(), "Monitor", ["Electronics"], "27-inch 4k monitor", "monitor.jpg", 300),
            new Product( Guid.NewGuid(), "CPU", ["Electronics"], "Intel Core i7", "cpu.jpg", 150),
            new Product( Guid.NewGuid(), "GPU", ["Electronics"], "Nvidia RTX 3080", "gpu.jpg", 500),
            new Product( Guid.NewGuid(), "Motherboard", ["Electronics"], "Asus X570", "motherboard.jpg", 200),
            new Product( Guid.NewGuid(), "RAM", ["Electronics"], "32GB DDR4 3200MHz", "ram.jpg", 100),
            new Product( Guid.NewGuid(), "SSD", ["Electronics"], "1TB NVMe SSD", "ssd.jpg", 100),
            new Product( Guid.NewGuid(), "HDD", ["Electronics"], "2TB 7200RPM HDD", "hdd.jpg", 50),

        ];

        return products;
    }

}