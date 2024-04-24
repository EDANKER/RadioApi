using Api.Data.Repository.CacheRepository;
using Api.Model.ResponseModel.MicroController;

namespace Api.Services.HebrideanCacheServices;

public interface IHebrideanCacheServices
{
    Task<string?> GetId(string key);
    Task<string?> GetLimit(string key);
    Task<bool> Refresh(string key);
    Task<bool> DeleteId(string key);
    Task<bool> Put(string key, string item);
    Task<bool> Search(string key);
}

public class HebrideanCacheServices(ICacheRepository hebrideanCacheRepository)
    : IHebrideanCacheServices
{
    public async Task<string?> GetId(string key)
    {
        return await hebrideanCacheRepository.GetId(key);
    }

    public async Task<string?> GetLimit(string key)
    {
        return await hebrideanCacheRepository.GetLimit(key);
    }

    public async Task<bool> Refresh(string key)
    {
        return true;
    }

    public async Task<bool> DeleteId(string key)
    {
        return true;
    }

    public async Task<bool> Put(string key, string item)
    {
        return true;
    }

    public async Task<bool> Search(string key)
    {
        return await hebrideanCacheRepository.Search(key);
    }
}