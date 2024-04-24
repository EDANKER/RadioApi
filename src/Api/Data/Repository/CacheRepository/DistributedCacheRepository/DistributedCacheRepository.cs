using Microsoft.Extensions.Caching.Distributed;

namespace Api.Data.Repository.CacheRepository.DistributedCacheRepository;

public class DistributedCacheRepository(IDistributedCache distributedCache) : ICacheRepository
{
    public async Task<string?> GetId(string key)
    {
        try
        {
            return await distributedCache.GetStringAsync(key);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string?> GetLimit(string key)
    {
        try
        {
            return await distributedCache.GetStringAsync(key);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Refresh(string key)
    {
        try
        {
            await distributedCache.RefreshAsync(key);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteId(string key)
    {
        try
        {
            await distributedCache.RemoveAsync(key);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Put(string key, string item)
    {
        try
        {
            await distributedCache.SetStringAsync(key, item, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(2)
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Search(string key)
    {
        try
        {
            string? get = await distributedCache.GetStringAsync(key);

            if (get != null)
                return true;
            
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}