namespace Api.Data.Repository.CacheRepository.Selectively;

public class Selectively(ICacheRepository cacheRepository)
{
    public readonly ICacheRepository CacheRepository = cacheRepository;
}