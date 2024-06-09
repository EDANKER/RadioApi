using Api.Interface;
using Api.Model.RequestModel.Create.CreateMusic;
using Api.Model.RequestModel.Update.UpdateMusic;
using Api.Model.ResponseModel.MicroController;
using Api.Model.ResponseModel.Music;
using Api.Services.HebrideanCacheServices;
using Api.Services.MicroControllerServices;
using Api.Services.MusicPlayerToMicroControllerServices;
using Api.Services.TimeCounterServices;

namespace Api.Services.MusicServices;

public interface IMusicServices
{
    Task<bool> Play(int idMusic, int[] idController);
    Task<bool> PlayLife(IFormFile formFile, int[] idController);
    Task<bool> Stop(int[] idController);
    Task<bool> CreateOrSave(string item, IFormFile formFile, string namePlayList);
    Task<List<DtoMusic>?> GetLimit(string item, int currentPage, int limit);
    Task<DtoMusic?> GetId(string item, int id);
    Task<List<DtoMusic>?> GetField(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id);
    Task<bool> UpdateId(string item, UpdateMusic updateMusic, int id);
    Task<bool> Search(string item, string name, string field);
}

public class MusicServices(
    IRepository<CreateMusic, DtoMusic, UpdateMusic> musicRepository,
    IFileServices.IFileServices fileServices,
    IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices,
    IMicroControllerServices microControllerServices,
    ITimeCounterServices timeCounterServices,
    IHebrideanCacheServices hebrideanCacheServices) : IMusicServices
{
    private async Task<Stream?> GetMusicInMinio(int id)
    {
        DtoMusic? dtoMusic = await musicRepository.GetId("Musics", id);
        if (dtoMusic != null)
        {
            string name = dtoMusic.Name;
            Stream? read = await fileServices.GetStream(name, "music");
            if (read != null)
                return read;
        }

        return null;
    }


    public async Task<bool> Play(int idMusic, int[] idController)
    {
        Stream? stream = await GetMusicInMinio(idMusic);
        if (stream != null)
        {
            foreach (var data in idController)
            {
                if (data < 0)
                    continue;
                DtoMicroController? dtoMicroController = await microControllerServices.GetId("MicroControllers", data);
                if (dtoMicroController != null)
                    if (await musicPlayerToMicroControllerServices.Play(
                            dtoMicroController, stream))
                        await hebrideanCacheServices.Put(idMusic.ToString(), "Играет");
            }
        }

        return false;
    }

    public async Task<bool> PlayLife(IFormFile formFile, int[] idController)
    {
        foreach (var data in idController)
        {
            if (data < 0)
                continue;
            DtoMicroController? dtoMicroController = await microControllerServices.GetId("MicroControllers", data);
            if (dtoMicroController != null)
                return await musicPlayerToMicroControllerServices.PlayLife(
                    dtoMicroController, formFile.OpenReadStream());
        }

        return false;
    }

    public async Task<bool> Stop(int[] idController)
    {
        foreach (var data in idController)
        {
            if (data < 0)
                continue;

            DtoMicroController? dtoMicroController = await microControllerServices.GetId("MicroControllers", data);
            if (dtoMicroController != null)
                return await musicPlayerToMicroControllerServices.Stop(dtoMicroController);
        }

        return false;
    }

    public async Task<bool> CreateOrSave(string item, IFormFile formFile, string namePlayList)
    {
        if (await musicRepository.CreateOrSave(item, new CreateMusic(formFile.FileName, namePlayList, await timeCounterServices.TimeToMinutes(formFile))))
            return await fileServices.Save(formFile, formFile.FileName, "music", "audio/mpeg");
        
        return false;
    }

    public async Task<List<DtoMusic>?> GetLimit(string item, int currentPage, int limit)
    {
        return await musicRepository.GetLimit(item, currentPage, limit);
    }

    public async Task<DtoMusic?> GetId(string item, int id)
    {
        return await musicRepository.GetId(item, id);
    }

    public async Task<List<DtoMusic>?> GetField(string item, string namePurpose, string field)
    {
        return await musicRepository.GetField(item, namePurpose, field);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        DtoMusic? dtoMusic = await GetId(item, id);
        if (dtoMusic != null && await fileServices.Delete(dtoMusic.Name, "music"))
            return await musicRepository.DeleteId(item, id);

        return false;
    }

    public async Task<bool> UpdateId(string item, UpdateMusic updateMusic, int id)
    {
        return await musicRepository.UpdateId(item, updateMusic, id);
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await musicRepository.Search(item, name, field);
    }
}