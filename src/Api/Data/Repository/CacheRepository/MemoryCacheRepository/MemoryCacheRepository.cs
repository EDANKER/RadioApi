using Microsoft.Extensions.Caching.Memory;

namespace Api.Data.Repository.CacheRepository.MemoryCacheRepository;

public class MemoryCacheRepository(IMemoryCache memoryCache) : ICacheRepository
{
    public Task<string?> GetId(string key)
    {
        try
        {
            return Task.FromResult(memoryCache.Get<string>(key));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<string?> GetLimit(string key)
    {
        try
        {
            return Task.FromResult(memoryCache.Get<string>(key));
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
            memoryCache.Remove(key);
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
            memoryCache.Remove(key);
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

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return true;
    }
}