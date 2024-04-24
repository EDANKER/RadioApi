namespace Api.Data.Repository.CacheRepository;

public interface ICacheRepository
{
    Task<string?> GetId(string key);
    Task<string?> GetLimit(string key);
    Task Refresh(string key);
    Task DeleteId(string key);
    Task Put(string key, string item);
    Task<bool> Search(string key);
}