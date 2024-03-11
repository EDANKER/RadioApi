using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace Api.Services.HebrideanCacheServices;

public interface IHebrideanCacheServices
{
    Task<string> Get(string key, string item);
    Task<bool> Refresh(string key);
    Task<bool> Delete(string key);
    Task<bool> Put(string key, string item);
}

public class HebrideanCacheServices : IHebrideanCacheServices
{
    private IDistributedCache _distributedCache;
    private ILogger<HebrideanCacheServices> _logger;
    private IMemoryCache _memoryCache;
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public HebrideanCacheServices(IConnectionMultiplexer connectionMultiplexer,IDistributedCache distributedCache,
        ILogger<HebrideanCacheServices> logger, IMemoryCache memoryCache)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _distributedCache = distributedCache;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task<string> Get(string key, string item)
    {
        try
        {
            return await _distributedCache.GetStringAsync(key);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            _memoryCache.Get(key);
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
            Console.WriteLine(e);
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
            Console.WriteLine(e);
        }

        return false;
    }

    public async Task<bool> Put(string key, string item)
    {
        try
        {
            await _distributedCache.SetStringAsync(key, item, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            _memoryCache.Set(key, item, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
        }

        return false;
    }
}