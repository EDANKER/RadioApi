using Api.Model.ResponseModel.MicroController;
using Newtonsoft.Json;

namespace Api.Services.JsonServices;

public interface IJsonServices<T>
{
    Task<string>? SerJson(T item);
    Task<T?>? DesJson(string item);
}

public class JsonServices<T>(ILogger<JsonServices<T>> logger) : IJsonServices<T>
{
    public Task<string>? SerJson(T item)
    {
        try
        {
            return Task.FromResult(JsonConvert.SerializeObject(item));
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public Task<T?>? DesJson(string item)
    {
        try
        {
            return Task.FromResult(JsonConvert.DeserializeObject<T>(item));
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return default;
        }
    }
}