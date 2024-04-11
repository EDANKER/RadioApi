using Api.Services.MicroControllerServices;
using Api.Services.TcpServices;

namespace Api.Services.MusicPlayerToMicroControllerServices;

public interface IMusicPlayerToMicroControllerServices
{
    Task<bool> SoundVol(int vol);
    Task<bool> Play(string cabinet, string flor, string nameMusic);
    Task<bool> Stop();
}

public class MusicPlayerToMicroControllerServices(
    IHttpMicroControllerServices httpMicroControllerServices,
    IMicroControllerServices microControllerServices)
    : IMusicPlayerToMicroControllerServices
{
    public async Task<bool> SoundVol(int vol)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Play(string cabinet, string flor, string nameMusic)
    {
        return await httpMicroControllerServices.Post(cabinet, flor, nameMusic);
    }

    public async Task<bool> Stop()
    {
        return true;
    }
}