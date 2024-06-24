using System.Net;
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
            HttpWebRequest httpResponseMessage = (HttpWebRequest)WebRequest.Create("http://10.3.16.220:8000/example.mp3");
            httpResponseMessage.Headers.Add("Content-Type", "audio/mpeg");
            httpResponseMessage.Method = "PUT";
            httpResponseMessage.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(bytes));
            httpResponseMessage.ContentType = "audio/mpeg";

            Stream netStream = httpResponseMessage.GetRequestStream();
            stream.CopyTo(netStream);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpResponseMessage.GetResponse();
            Console.WriteLine(httpWebResponse.StatusCode);
            if (httpWebResponse.StatusCode.ToString() == "OK")
            {
                return true;
            }
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