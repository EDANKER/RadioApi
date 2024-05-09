using System.Text;
using Api.Model.ResponseModel.MicroController;
using Api.Services.StreamToByteArrayServices;

namespace Api.Services.HttpMicroControllerServices;

public interface IHttpMicroControllerServices
{
    public Task<bool> PostVol(DtoMicroController dtoMicroController, int idMusic);
    public Task<bool> PostByte(DtoMicroController dtoMicroController, Stream stream);
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
            Console.WriteLine(httpResponseMessage.Content.ReadAsStringAsync().Result);
            return true;
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
            byte[]? arrayByte = await streamToByteArrayServices.StreamToByte(stream);
            if (arrayByte == null)
                return false;
            
            
            httpClient.BaseAddress = new Uri($"https://{dtoMicroController.Ip}:{dtoMicroController.Port}");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("", new ByteArrayContent(arrayByte));
            Console.WriteLine(await httpResponseMessage.Content.ReadAsStringAsync());
            
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}