using Newtonsoft.Json;

namespace Api.Services.JsonServices;

public interface IJsonServices<T>
{
    string? SerJson(T item);
    T? DesJson(string item);
}

public class JsonServices<T>(ILogger<JsonServices<T>> logger) : IJsonServices<T>
{
    public string? SerJson(T item)
    {
        try
        {
            return JsonConvert.SerializeObject(item);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public T? DesJson(string item)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(item);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return default;
        }
    }
}