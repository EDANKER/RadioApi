using Api.Services.JsonServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Data.Repository.HebrideanCacheRepository;

public interface IHebrideanCacheServices<T>
{
    Task<string?> GetId(string key);
    Task<string?> GetLimit(string key);
    Task<bool> Refresh(string key);
    Task<bool> DeleteId(string key);
    Task<bool> Put(string key, string item);
    Task<bool> Search(string key);
}

public class HebrideanCacheRepository<T>(
    IJsonServices<T?> jsonServices,
    IDistributedCache distributedCache,
    ILogger<HebrideanCacheRepository<T>> logger,
    IMemoryCache memoryCache)
    : IHebrideanCacheServices<T>
{
    public async Task<string?> GetId(string key)
    {
        try
        {
            string? get = await distributedCache.GetStringAsync(key);
            if (get != null)
                return get;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            string? get = memoryCache.Get<string>(key);
            if (get != null) 
                return get;
        }
        return null;
    }

    public async Task<string?> GetLimit(string key)
    {
        try
        {
            string? get = await distributedCache.GetStringAsync(key);
            if (get != null)
                return get;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            string? get = (string?)memoryCache.Get(key);
            if (get != null)
                return get;
        }

        return null;
    }

    public async Task<bool> Refresh(string key)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }

        return false;
    }

    public async Task<bool> DeleteId(string key)
    {
        try
        {
            await distributedCache.RemoveAsync(key);
            return true;
        }
        catch (Exception e)
        {
            memoryCache.Remove(key);
            logger.LogError(e.ToString());
        }

        return false;
    }

    public async Task<bool> Put(string key, string item)
    {
        try
        {
            await distributedCache.SetStringAsync(key, item, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
            
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            memoryCache.Set(key, item, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
            return false;
        }
    }

    public async Task<bool> Search(string key)
    {
        try
        {

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}