using System.Net;
using System.Net.Sockets;
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
        try
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(IPAddress.Parse("10.3.16.220"), 8000);
            string request =
                $"SOURCE /example.mp3 HTTP/1.0\r\n Authorization: Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes("source:hackme"))}\r\n Content-Type: audio/mpeg\r\n\r\n";
            byte[] au = Encoding.ASCII.GetBytes(request);
            socket.Send(au, 0, au.Length, SocketFlags.None);
            byte[] buffer = new byte[4096];
            int byteRead;

            while ((byteRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                socket.Send(buffer, byteRead, SocketFlags.None);
            }

            socket.Close();

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