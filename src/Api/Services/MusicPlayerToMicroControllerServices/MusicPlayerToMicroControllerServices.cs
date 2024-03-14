using System.Net;
using System.Net.Sockets;

namespace Api.Services.MusicPlayerToMicroControllerServices;

public interface IMusicPlayerToMicroControllerServices
{
    Task<bool> SoundVol(int vol);
    Task<bool> PlayMore(Stream memoryStream, List<string> florSector);
    Task<bool> PlayOne(Stream memoryStream, List<string> florSector);
    Task<bool> Stop();
}

public class MusicPlayerToMicroControllerServices(ILogger<MusicPlayerToMicroControllerServices> logger, IAudioFileServices.IAudioFileServices audioFileServices)
    : IMusicPlayerToMicroControllerServices
{

    public async Task<bool> SoundVol(int vol)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> PlayMore(Stream memoryStream, List<string> florSector)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> PlayOne(Stream memoryStream, List<string> florSector)
    {
        try
        {
            byte[] buffer = new byte[1024];
            TcpClient tcpClient = new TcpClient();
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8060);
            await tcpClient.ConnectAsync(ipEndPoint.Address, ipEndPoint.Port);
            int readAsync;
            do
            {
                readAsync = await memoryStream.ReadAsync(buffer, 0, buffer.Length);
                if (readAsync > 0)
                {
                    await tcpClient.GetStream().WriteAsync(buffer, 0, buffer.Length);
                }
            } while (readAsync > 0);
            memoryStream.Close();
            tcpClient.Close();
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> Stop()
    {
        return true;
    }
}