using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extensions
{
    public static async Task<IApplicationBuilder> ApplyMigrationAsync(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        await using var context = serviceScope.ServiceProvider.GetService<DiscountDbContext>() ?? throw new ArgumentException("DbContext isn't registered.");
        await context.Database.MigrateAsync();
        return app;
    }
}