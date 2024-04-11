using Api.Data.Minio;
using Api.Data.Repository.MicroController;
using Api.Data.Repository.Music;
using Api.Model.MinioModel;
using Api.Model.RequestModel.MicroController;
using Api.Model.ResponseModel.MicroController;
using Api.Services.HebrideanCacheServices;
using Api.Services.TcpServices;

namespace Api.Services.MicroControllerServices;

public interface IMicroControllerServices
{
    Task<byte[]?> GetMusicInMinio(int id);
    Task<bool> CreateOrSave(string item, MicroController microController);
    Task<List<DtoMicroController>> GetLimit(string item, int limit);
    Task<DtoMicroController> GetId(string item, string[] florSector);
    Task<bool> DeleteId(string item, int id);
    Task<bool> Update(string item, MicroController microController);
    Task<bool> Search(string item, string name);
    Task<bool> CheckMicroController(string cabinet, string flor, string nameMusic);
}

public class MicroControllerServices(
    IMicroControllerRepository controllerRepository,
    IAudioFileServices.IAudioFileServices audioFileServices,
    IHttpMicroControllerServices httpMicroControllerServices,
    IMusicRepository musicRepository,
    IHebrideanCacheServices hebrideanCacheServices)
    : IMicroControllerServices
{
    public async Task<byte[]?> GetMusicInMinio(int id)
    {
        byte[] buffer = new byte[1024];
        int? read;
        read = await audioFileServices.GetByteMusic(musicRepository.GetId("Musics", id).Result.Name).Result
            .ReadAsync(buffer, 0, buffer.Length);

        if (read > 0)
            return buffer;

        return null;
    }

    public async Task<bool> CreateOrSave(string item, MicroController microController)
    {
        if (!await controllerRepository.CreateOrSave(item, microController))
            return false;

        return await hebrideanCacheServices.Put("1", "Hello");
    }

    public async Task<List<DtoMicroController>> GetLimit(string item, int limit)
    {
        return await controllerRepository.GetLimit(item, limit);
    }

    public async Task<DtoMicroController> GetId(string item, string[] florSector)
    {
        return await controllerRepository.GetId(item, florSector);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await controllerRepository.DeleteId(item, id);
    }

    public async Task<bool> Update(string item, MicroController microController)
    {
        return await controllerRepository.Update(item, microController);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await controllerRepository.Search(item, name);
    }

    public async Task<bool> CheckMicroController(string cabinet, string flor, string nameMusic)
    {
        return await httpMicroControllerServices.Post(cabinet, flor, nameMusic);
    }
}