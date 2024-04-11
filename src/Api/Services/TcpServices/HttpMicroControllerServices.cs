namespace Api.Services.TcpServices;

public interface IHttpMicroControllerServices
{
    public Task<bool> Post(string cabinet, string flor, string nameMusic);
}

public class HttpMicroControllerServices(ILogger<HttpMicroControllerServices> logger, HttpClient httpClient) : IHttpMicroControllerServices
{
    public async Task<bool> Post(string cabinet, string flor, string nameMusic)
    {
        try
        {
            
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}