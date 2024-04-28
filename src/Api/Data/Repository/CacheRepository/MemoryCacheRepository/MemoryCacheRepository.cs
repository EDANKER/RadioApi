using Microsoft.Extensions.Caching.Memory;

namespace Api.Data.Repository.CacheRepository.MemoryCacheRepository;

public class MemoryCacheRepository(IMemoryCache memoryCache) : ICacheRepository
{
    public Task<string?> GetId(string key)
    {
        string? get = memoryCache.Get<string>(key);

        if (get != null)
            return Task.FromResult(get);

        return Task.FromResult(get);
    }

    public Task<string?> GetLimit(string key)
    {
        string? get = memoryCache.Get<string>(key);

        if (get != null)
            return Task.FromResult(get);

        return Task.FromResult(get);
    }

    public Task? Refresh(string key)
    {
        memoryCache.Remove(key);
        return null;
    }

    public async Task DeleteId(string key)
    {
        memoryCache.Remove(key);
    }

    public async Task Put(string key, string item)
    {
        memoryCache.Set(key, item);
    }

    public Task<bool> Search(string key)
    {
        string? get = memoryCache.Get<string>(key);

        if (get != null)
            return Task.FromResult(true);

        return Task.FromResult(false);
    }
}