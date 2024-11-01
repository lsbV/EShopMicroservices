using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace BuildingBlocks.Extensions;

public static class DistributedCacheExtension
{
    public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(key)) return default;
        var data = await cache.GetStringAsync(key, cancellationToken);
        return data is null ? default : JsonSerializer.Deserialize<T>(data);
    }
    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(key)) return;
        var data = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, data, cancellationToken);
    }

}