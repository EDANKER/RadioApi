using Api.Services.JsonServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace Api.Services.HebrideanCacheServices;

public interface IHebrideanCacheServices<T>
{
    Task<T?> GetId(string key);
    Task<List<T>?> GetLimit(string key);
    Task<bool> Refresh(string key);
    Task<bool> DeleteId(string key);
    Task<bool> Put(string key, T item);
    Task<bool> Search(string key);
}

public class HebrideanCacheServices<T>(
    IDistributedCache distributedCache,
    ILogger<HebrideanCacheServices<T>> logger,
    IMemoryCache memoryCache)
    : IHebrideanCacheServices<T>
{
   
    
    private IJsonServices<T> _jsonServices = new JsonServices<T>();
    
    public async Task<T?> GetId(string key)
    {
        
        try
        {
            string? get = await distributedCache.GetStringAsync(key);
            if (get != null)
                return _jsonServices.DesJson(get);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            string? get = memoryCache.Get<string>(key);
            if (get != null) 
                return _jsonServices.DesJson(get);
        }
        return default;
    }

    public async Task<List<T>?> GetLimit(string key)
    {
        List<T> dtoMicroControllers = new List<T>();
        
        try
        {
            string? get = await distributedCache.GetStringAsync(key);
            if (get != null)
                dtoMicroControllers.Add(_jsonServices.DesJson(get));
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            string? get = (string?)memoryCache.Get(key);
            if (get != null) 
                dtoMicroControllers.Add(_jsonServices.DesJson(get));
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
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }

        return false;
    }

    public async Task<bool> Put(string key, T item)
    {
        try
        {
            await distributedCache.SetStringAsync(key, _jsonServices.SerJson(item), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            memoryCache.Set(key, item, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
        }

        return false;
    }

    public Task<bool> Search(string key)
    {
        throw new NotImplementedException();
    }
}