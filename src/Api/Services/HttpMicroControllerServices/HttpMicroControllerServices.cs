using System.Text;
using Api.Model.ResponseModel.MicroController;

namespace Api.Services.HttpMicroControllerServices;

public interface IHttpMicroControllerServices
{
    Task<bool> PostVol(DtoMicroController dtoMicroController, int vol);
    Task<bool> Play(DtoMicroController dtoMicroController);
    Task<bool> PostStream(Stream stream);
    Task<bool> Stop(DtoMicroController dtoMicroController);
}

public class HttpMicroControllerServices(
    ILogger<HttpMicroControllerServices> logger,
    HttpClient httpClient)
    : IHttpMicroControllerServices
{
    public async Task<bool> PostVol(DtoMicroController dtoMicroController, int vol)
    {
        try
        {
            httpClient.BaseAddress = new Uri($"https://{dtoMicroController.Ip}:{dtoMicroController.Port}");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("",
                new StringContent(vol.ToString(), Encoding.UTF8,
                    "text/plain"));
            return httpResponseMessage.Content.ReadAsStringAsync().Result == "готово";
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> Play(DtoMicroController dtoMicroController)
    {
        try
        {
            httpClient.BaseAddress = new Uri($"https://{dtoMicroController.Ip}:{dtoMicroController.Port}");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("", new StringContent("start"));

            return await httpResponseMessage.Content.ReadAsStringAsync() == "start";
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> PostStream(Stream stream)
    {
        string au = "source:hackme";
        byte[] bytes = Encoding.ASCII.GetBytes(au);
        
        try
        {
            httpClient.BaseAddress = new Uri("http://10.3.16.220:8080");
            StreamContent streamContent = new StreamContent(stream);
            streamContent.Headers.Add("Content-Type", "audio/mpeg");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(bytes));
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("/stream", streamContent);

            Console.WriteLine(httpResponseMessage.StatusCode);
            if (httpResponseMessage.StatusCode.ToString() == "OK")
            {
                stream.Close();
                return true;
            }

            stream.Close();
            return false;
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