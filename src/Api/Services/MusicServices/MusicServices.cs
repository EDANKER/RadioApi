using Api.Interface;
using Api.Model.RequestModel.Create.CreateMusic;
using Api.Model.RequestModel.Update.UpdateMusic;
using Api.Model.ResponseModel.MicroController;
using Api.Model.ResponseModel.Music;
using Api.Services.MicroControllerServices;
using Api.Services.MusicPlayerToMicroControllerServices;
using Api.Services.StreamToByteArrayServices;
using Api.Services.TimeCounterServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Services.MusicServices;

public interface IMusicServices
{
    Task<Stream?> GetMusicInMinio(int id);
    Task<bool> Play(int idMusic, int[] idController);
    Task<bool> PlayLife(IFormFile formFile, int[] idController);
    Task<bool> Stop(int[] idController);
    Task<bool> CreateOrSave(string item, IFormFile formFile, string namePlayList);
    Task<List<DtoMusic>?> GetLimit(string item, int limit);
    Task<DtoMusic?> GetId(string item, int id);
    Task<List<DtoMusic>?> GetUni(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id, string path);
    Task<bool> UpdateId(string item, UpdateMusic updateMusic, int id);
    Task<bool> Search(string item, string name, string field);
}

public class MusicServices(
    IRepository<CreateMusic, DtoMusic, UpdateMusic> musicRepository,
    IFileServices.IFileServices fileServices,
    IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices,
    IMicroControllerServices microControllerServices,
    IStreamToByteArrayServices streamToByteArrayServices,
    ITimeCounterServices timeCounterServices) : IMusicServices
{
    public async Task<Stream?> GetMusicInMinio(int id)
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
                    await musicPlayerToMicroControllerServices.Play(
                        dtoMicroController, stream);
            }
        }
        else
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> PlayLife(IFormFile formFile, int[] idController)
    {
        foreach (var data in idController)
        {
            if (data < 0)
                continue;
            DtoMicroController? dtoMicroController = await microControllerServices.GetId("MicroControllers", data);
            if (dtoMicroController != null)
                await musicPlayerToMicroControllerServices.PlayLife(
                    dtoMicroController, formFile.OpenReadStream());
        }

        return true;
    }

    public async Task<bool> Stop( int[] idController)
    {
        return await musicPlayerToMicroControllerServices.Stop();
    }

    public async Task<bool> CreateOrSave(string item, IFormFile formFile, string namePlayList)
    {
        if (await musicRepository.CreateOrSave(item,
                new CreateMusic(formFile.FileName, namePlayList, await timeCounterServices.Time(formFile))))
        {
            List<DtoMusic>? dtoMusics = await musicRepository.GetString(item, formFile.FileName, "Name");
            if (dtoMusics != null)
                foreach (var data in dtoMusics)
                    return await fileServices.Save(formFile, data.Name, "music", "audio/mpeg");
        }

        return false;
    }

    public async Task<List<DtoMusic>?> GetLimit(string item, int limit)
    {
        return await musicRepository.GetLimit(item, limit);
    }

    public async Task<DtoMusic?> GetId(string item, int id)
    {
        return await musicRepository.GetId(item, id);
    }

    public async Task<List<DtoMusic>?> GetUni(string item, string namePurpose, string field)
    {
        return await musicRepository.GetString(item, namePurpose, field);
    }

    public async Task<bool> DeleteId(string item, int id, string path)
    {
        if (await fileServices.Delete(id.ToString(), "music"))
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