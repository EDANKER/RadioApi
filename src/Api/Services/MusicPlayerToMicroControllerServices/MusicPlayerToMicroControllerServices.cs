using Api.Model.ResponseModel.MicroController;
using Api.Services.HttpMicroControllerServices;
namespace Api.Services.MusicPlayerToMicroControllerServices;

public interface IMusicPlayerToMicroControllerServices
{
    Task<bool> SoundVol(DtoMicroController dtoMicroController, int vol);
    Task<bool> Play(DtoMicroController dtoMicroController, Stream stream);
    Task<bool> PlayLife(DtoMicroController dtoMicroController, Stream stream);
    Task<bool> Stop();
}

public class MusicPlayerToMicroControllerServices(
    IHttpMicroControllerServices httpMicroControllerServices)
    : IMusicPlayerToMicroControllerServices
{
    public async Task<bool> SoundVol(DtoMicroController dtoMicroController, int vol)
    {
        return await httpMicroControllerServices.PostVol(dtoMicroController, vol);
    }

    public async Task<bool> Play(DtoMicroController dtoMicroController, Stream stream)
    {
        return await httpMicroControllerServices.PostByte(dtoMicroController, stream);
    }

    public async Task<bool> PlayLife(DtoMicroController dtoMicroController, Stream stream)
    {
        return await httpMicroControllerServices.PostByte(dtoMicroController, stream);
    }

    public async Task<bool> Stop()
    {
        return true;
    }
}