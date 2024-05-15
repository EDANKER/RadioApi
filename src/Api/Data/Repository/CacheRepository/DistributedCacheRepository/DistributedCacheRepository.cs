using Microsoft.Extensions.Caching.Distributed;

namespace Api.Data.Repository.CacheRepository.DistributedCacheRepository;

public class DistributedCacheRepository(IDistributedCache distributedCache, ILogger<DistributedCacheRepository> logger) : ICacheRepository
{
    public async Task<string?> GetId(string key)
    {
        try
        {
            string? get = await distributedCache.GetStringAsync(key);
            if (get != null)
                return get;

            return get;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }
    
    public async Task<bool> Refresh(string key)
    {
        try
        {
            await distributedCache.RefreshAsync(key);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
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
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> Put(string key, string item)
    {
        try
        {
            await distributedCache.SetStringAsync(key, item, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}