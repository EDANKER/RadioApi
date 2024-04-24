namespace Api.Data.Repository.CacheRepository.Selectively;

public class Selectively(ICacheRepository cacheRepository)
{
    public ICacheRepository CacheRepository = cacheRepository;
}