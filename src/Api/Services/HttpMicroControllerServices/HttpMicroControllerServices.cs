namespace Api.Services.HttpMicroControllerServices;

public interface IHttpMicroControllerServices
{
    public Task<bool> Post(int cabinet, int floor, int idMusic);
}

public class HttpMicroControllerServices(
    ILogger<HttpMicroControllerServices> logger)
    : IHttpMicroControllerServices
{
    public async Task<bool> Post(int cabinet, int floor, int idMusic)
    {
        HttpClient httpClient = new HttpClient();
        
        try
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "");
            await httpClient.SendAsync(httpRequestMessage);

            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}