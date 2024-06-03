using System.Text;
using Api.Model.ResponseModel.MicroController;
using Api.Services.StreamToByteArrayServices;

namespace Api.Services.HttpMicroControllerServices;

public interface IHttpMicroControllerServices
{
    Task<bool> PostVol(DtoMicroController dtoMicroController, int idMusic);
    Task<bool> PostByte(DtoMicroController dtoMicroController, Stream stream);
    Task<bool> Stop(DtoMicroController dtoMicroController);
}

public class HttpMicroControllerServices(
    ILogger<HttpMicroControllerServices> logger,
    IStreamToByteArrayServices streamToByteArrayServices)
    : IHttpMicroControllerServices
{
    private HttpClient httpClient;

    public async Task<bool> PostVol(DtoMicroController dtoMicroController, int idMusic)
    {
        httpClient = new HttpClient();
        try
        {
            httpClient.BaseAddress = new Uri($"https://{dtoMicroController.Ip}:{dtoMicroController.Port}");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("",
                new StringContent(idMusic.ToString(), Encoding.UTF8,
                    "application/json"));
            return httpResponseMessage.Content.ReadAsStringAsync().Result == "готово";
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> PostByte(DtoMicroController dtoMicroController, Stream stream)
    {
        httpClient = new HttpClient();

        try
        {
            httpClient.BaseAddress = new Uri($"https://{dtoMicroController.Ip}:{dtoMicroController.Port}");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("", new StreamContent(stream));
            Console.WriteLine(await httpResponseMessage.Content.ReadAsStringAsync());

            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> Stop(DtoMicroController dtoMicroController)
    {
        try
        {
            httpClient = new HttpClient();
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("", 
                new StringContent("stop", Encoding.UTF8,
                    "application/json"));
            return await httpResponseMessage.Content.ReadAsStringAsync() == "stop";
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}