namespace Api.Data.Repository.CacheRepository.HebrideanCacheRepository;

public class HebrideanCacheRepository(
    ILogger<HebrideanCacheRepository> logger,
    ICacheRepository distributedCache,
    ICacheRepository memoryCache)
{
    private Selectively.Selectively _selectivelyD =
        new(distributedCache);
    private Selectively.Selectively _selectivelyM =
        new(memoryCache);

    private static async Task<bool> Connect()
    {
        return false;
    }

    public async Task<string?> GetId(string key)
    {
        try
        {
            if (await Connect())
                return await _selectivelyD.CacheRepository.GetId(key);

            return await _selectivelyM.CacheRepository.GetId(key);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<string?> GetLimit(string key)
    {
        try
        {
            if (await Connect())
                return await _selectivelyD.CacheRepository.GetLimit(key);

            return await _selectivelyM.CacheRepository.GetLimit(key);
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
            if (await Connect())
                await _selectivelyD.CacheRepository.Refresh(key);

            await _selectivelyM.CacheRepository.Refresh(key);

            return true;
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
            if (await Connect())
                await _selectivelyD.CacheRepository.DeleteId(key);

            await _selectivelyM.CacheRepository.DeleteId(key);

            return true;
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
            if (await Connect())
                await _selectivelyD.CacheRepository.Put(key, item);

            await _selectivelyM.CacheRepository.Put(key, item);

            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> Search(string key)
    {
        try
        {
            if (await Connect())
                return await _selectivelyD.CacheRepository.Search(key);
            
            return await _selectivelyM.CacheRepository.Search(key);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}