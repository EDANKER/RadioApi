using System.Text;
using Api.Model.ResponseModel.MicroController;

namespace Api.Services.HttpMicroControllerServices;

public interface IHttpMicroControllerServices
{
    public Task<bool> Post(DtoMicroController dtoMicroController, int idMusic);
}

public class HttpMicroControllerServices(
    ILogger<HttpMicroControllerServices> logger)
    : IHttpMicroControllerServices
{
    public async Task<bool> Post(DtoMicroController dtoMicroController, int idMusic)
    {
        HttpClient httpClient = new HttpClient();
        try
        {
            httpClient.BaseAddress = new Uri($"https://{dtoMicroController.Ip}:{dtoMicroController.Port}");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("/api/v1/Music/Post", 
                new StringContent(idMusic.ToString(), Encoding.UTF8, 
                    "application/json"));
            Console.WriteLine(httpResponseMessage.Content.ReadAsStringAsync().Result);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}