namespace Api.Data.Repository.CacheRepository;

public interface ICacheRepository
{
    Task<string?> GetId(string key);
    Task<bool> Refresh(string key);
    Task<bool> DeleteId(string key);
    Task<bool> Put(string key, string item);
}