using Api.Model.ResponseModel.MicroController;
using Api.Services.HttpMicroControllerServices;
namespace Api.Services.MusicPlayerToMicroControllerServices;

public interface IMusicPlayerToMicroControllerServices
{
    Task<bool> SoundVol(int vol);
    Task<bool> Play(DtoMicroController dtoMicroController, int idMusic);
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

    public async Task<bool> Play(DtoMicroController dtoMicroController, int idMusic)
    {
        return await httpMicroControllerServices.Post(dtoMicroController, idMusic);
    }

    public async Task<bool> Stop()
    {
        return true;
    }
}