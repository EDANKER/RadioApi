using System.Net;
using System.Text;
using Api.Model.ResponseModel.FloorMicroController;
using NAudio.Wave;

namespace Api.Services.HttpMicroControllerServices;

public interface IHttpMicroControllerServices
{
    Task<bool> Play(DtoMicroController dtoMicroController);
    Task<bool> PostStream(Stream stream);
    Task<bool> Stop(DtoMicroController dtoMicroController);
}

public class HttpMicroControllerServices(
    ILogger<HttpMicroControllerServices> logger,
    HttpClient httpClient)
    : IHttpMicroControllerServices
{
    public async Task<bool> Play(DtoMicroController dtoMicroController)
    {
        try
        {
            httpClient.BaseAddress = new Uri($"http://{dtoMicroController.Ip}:{dtoMicroController.Port}");
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
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.3.16.220:8000/example.mp3");
            httpWebRequest.Method = "PUT";
            httpWebRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(bytes));
            httpWebRequest.Headers.Add("Content-Type", "audio/mpeg");

            Stream netStream = await httpWebRequest.GetRequestStreamAsync();

            byte[] buffer = new byte[8192];
            int byteRead;
            
            while ((byteRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                await netStream.WriteAsync(buffer, 0, byteRead);
            HttpWebResponse httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            if (httpWebResponse.StatusCode.ToString() == "OK")
                return true;
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