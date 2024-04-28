using Microsoft.Extensions.Caching.Distributed;

namespace Api.Data.Repository.CacheRepository.DistributedCacheRepository;

public class DistributedCacheRepository(IDistributedCache distributedCache) : ICacheRepository
{
    public async Task<string?> GetId(string key)
    {
        string? get = await distributedCache.GetStringAsync(key);
        if (get != null)
            return get;

        return null;
    }

    public async Task<string?> GetLimit(string key)
    {
        string? get = await distributedCache.GetStringAsync(key);
        if (get != null)
            return get;

        return null;
    }

    public async Task? Refresh(string key)
    {
        await distributedCache.RefreshAsync(key);
    }

    public async Task DeleteId(string key)
    {
        await distributedCache.RemoveAsync(key);
    }

    public async Task Put(string key, string item)
    {
        await distributedCache.SetStringAsync(key, item, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        });
    }

    public async Task<bool> Search(string key)
    {
        string? get = await distributedCache.GetStringAsync(key);

        if (get != null)
            return true;

        return false;
    }
}