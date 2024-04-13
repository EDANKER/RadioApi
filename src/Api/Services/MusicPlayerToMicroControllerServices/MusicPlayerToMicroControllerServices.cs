using Api.Services.HttpMicroControllerServices;
using Api.Services.MicroControllerServices;

namespace Api.Services.MusicPlayerToMicroControllerServices;

public interface IMusicPlayerToMicroControllerServices
{
    Task<bool> SoundVol(int vol);
    Task<bool> Play(int cabinet, int floor, string nameMusic);
    Task<bool> Stop();
}

public class MusicPlayerToMicroControllerServices(
    IHttpMicroControllerServices httpMicroControllerServices)
    : IMusicPlayerToMicroControllerServices
{
    public async Task<bool> SoundVol(int vol)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Play(int cabinet, int floor, string nameMusic)
    {
        return await httpMicroControllerServices.Post(cabinet, floor, nameMusic);
    }

    public async Task<bool> Stop()
    {
        return true;
    }
}