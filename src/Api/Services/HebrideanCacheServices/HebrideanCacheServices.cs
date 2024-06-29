using Api.Data.Repository.CacheRepository.HebrideanCacheRepository;

namespace Api.Services.HebrideanCacheServices;

public interface IHebrideanCacheServices
{
    Task<string?> GetId(string key);
    Task<bool> Refresh(string key);
    Task<bool> DeleteId(string key);
    Task<bool> Put(string key, string? item);

}

public class HebrideanCacheServices(HebrideanCacheRepository hebrideanCacheRepository)
    : IHebrideanCacheServices
{
    public async Task<string?> GetId(string key)
    {
        return await hebrideanCacheRepository.GetId(key);
    }
    
    public async Task<bool> Refresh(string key)
    {
        return await hebrideanCacheRepository.Refresh(key);
    }

    public async Task<bool> DeleteId(string key)
    {
        return await hebrideanCacheRepository.DeleteId(key);
    }

    public async Task<bool> Put(string key, string? item)
    {
        return await hebrideanCacheRepository.Put(key, item);
    }
}