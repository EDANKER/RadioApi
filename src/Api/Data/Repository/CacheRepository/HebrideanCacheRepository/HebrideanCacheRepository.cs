namespace Api.Data.Repository.CacheRepository.HebrideanCacheRepository;

public class HebrideanCacheRepository(
    ILogger<HebrideanCacheRepository> logger,
    ICacheRepository distributedCache)
{
    private Selectively.Selectively _selectivelyD =
        new(distributedCache);


    public async Task<string?> GetId(string key)
    {
        try
        {
            return await _selectivelyD.CacheRepository.GetId(key);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<bool> Refresh(string key)
    {
        try
        {
            return await _selectivelyD.CacheRepository.Refresh(key);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> DeleteId(string key)
    {
        try
        {
            return await _selectivelyD.CacheRepository.DeleteId(key);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> Put(string key, string item)
    {
        try
        {
            return await _selectivelyD.CacheRepository.Put(key, item);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}