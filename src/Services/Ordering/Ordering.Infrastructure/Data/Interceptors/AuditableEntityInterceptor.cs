using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntities(DbContext? context)
    {

        if (context is null)
        {
            return;
        }
        var entries = context.ChangeTracker.Entries<IEntity>();
        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
                entry.Entity.CreatedBy = "system";
            }
            if (entry.State is EntityState.Modified or EntityState.Added)
            {
                entry.Entity.LastModifiedAt = DateTime.Now;
                entry.Entity.LastModifiedBy = "system";
            }
        }
    }
}