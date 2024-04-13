using AutoMapper.Execution;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace Api.Services.HebrideanCacheServices;

public interface IHebrideanCacheServices
{
    Task<string?> Get(string key);
    Task<bool> Refresh(string key);
    Task<bool> Delete(string key);
    Task<bool> Put(string key, string item);
}

public class HebrideanCacheServices(
    IDistributedCache distributedCache,
    ILogger<HebrideanCacheServices> logger,
    IMemoryCache memoryCache)
    : IHebrideanCacheServices
{
    public async Task<string?> Get(string key)
    {
        try
        {
            string? get = await distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(get))
            {
                
                return null;
            }

            return get;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            memoryCache.Get(key);
        }

        return "";
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

    public async Task<bool> Delete(string key)
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

    public async Task<bool> Put(string key, string item)
    {
        try
        {
            await distributedCache.SetStringAsync(key, item, new DistributedCacheEntryOptions
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
}