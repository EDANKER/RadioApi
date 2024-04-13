using Newtonsoft.Json;

namespace Api.Services.JsonServices;

public interface IJsonServices<T>
{
    string? SerJson(T item);
    T? DesJson(string item);
}

public class JsonServices<T> : IJsonServices<T> where T : class
{
    private ILogger<JsonServices<T>> _logger;
    
    public string? SerJson(T item)
    {
        try
        {
            return JsonConvert.SerializeObject(item);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return null;
        }
    }

    public T? DesJson(string item)
    {
        try
        {
            return JsonConvert.DeserializeObject(item) as T;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return default;
        }
    }
}