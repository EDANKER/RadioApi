using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Data.Repository.CacheRepository.HebrideanCacheRepository;

public class HebrideanCacheRepository(
    ILogger<HebrideanCacheRepository> logger,
    IDistributedCache distributedCache,
    IMemoryCache memoryCache)
    : ICacheRepository
{
    private Selectively.Selectively _selectivelyD =
        new(new DistributedCacheRepository.DistributedCacheRepository(distributedCache));
    private Selectively.Selectively _selectivelyM =
        new(new MemoryCacheRepository.MemoryCacheRepository(memoryCache));

    public async Task<bool> Connect()
    {
        return false;
    }

    public async Task<string?> GetId(string key)
    {
        try
        {
            return await _selectivelyD.CacheRepository.GetId(key);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<string?> GetLimit(string key)
    {
        try
        {
            return null;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task Refresh(string key)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }
    }

    public async Task DeleteId(string key)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            
        }
    }

    public async Task Put(string key, string item)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            
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