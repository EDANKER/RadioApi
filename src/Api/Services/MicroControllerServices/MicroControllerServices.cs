using Api.Data.Repository.MicroController;
using Api.Data.Repository.Music;
using Api.Model.RequestModel.MicroController;
using Api.Model.ResponseModel.MicroController;
using Api.Services.HebrideanCacheServices;
using Api.Services.HttpMicroControllerServices;
using Api.Services.JsonServices;

namespace Api.Services.MicroControllerServices;

public interface IMicroControllerServices
{
    Task<byte[]?> GetMusicInMinio(int id);
    Task<bool> CreateOrSave(string item, MicroController microController);
    Task<List<DtoMicroController>> GetLimit(string item, int limit);
    Task<DtoMicroController> GetId(string item, int id);
    Task<bool> DeleteId(string item, int id);
    Task<bool> Update(string item, MicroController microController);
    Task<bool> Search(string item, string name);
    Task<bool> CheckMicroController(int id);
}

public class MicroControllerServices(
    IMicroControllerRepository controllerRepository,
    IAudioFileServices.IAudioFileServices audioFileServices,
    IHttpMicroControllerServices httpMicroControllerServices,
    IMusicRepository musicRepository,
    IHebrideanCacheServices<DtoMicroController> hebrideanCacheServices)
    : IMicroControllerServices
{
    public async Task<byte[]?> GetMusicInMinio(int id)
    {
        Stream read = audioFileServices.GetByteMusic(musicRepository.GetId("Musics", id).Result.Name).Result;
        byte[] buffer = new byte[read.Length];
        var readAsync = await read.ReadAsync(buffer, 0, buffer.Length);
        if (readAsync > 0)
            return buffer;

        return null;
    }

    public async Task<bool> CreateOrSave(string item, MicroController microController)
    {
        if (!await controllerRepository.CreateOrSave(item, microController))
            return false;

        DtoMicroController dtoMicroController = await controllerRepository.GetName(item, microController.Name);
        return await hebrideanCacheServices.Put(dtoMicroController.Id.ToString(), dtoMicroController);
    }

    public async Task<List<DtoMicroController>> GetLimit(string item, int limit)
    {
        for (int i = 0; i < limit; i++)
        {
            List<DtoMicroController>? dtoMicroController = await hebrideanCacheServices.GetLimit(i.ToString());
            if (dtoMicroController != null)
                return dtoMicroController;
        }
        
        return await controllerRepository.GetLimit(item, limit);
    }

    public async Task<DtoMicroController> GetId(string item, int id)
    {
        DtoMicroController? dtoMicroController = await hebrideanCacheServices.GetId(id.ToString());

        if (dtoMicroController != default)
            return dtoMicroController;
        

        return await controllerRepository.GetId(item, id);
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

    public Task<bool> CheckMicroController(int id)
    {
        throw new NotImplementedException();
    }
}