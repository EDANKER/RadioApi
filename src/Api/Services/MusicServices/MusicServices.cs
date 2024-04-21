using Api.Data.Repository.Music;
using Api.Interface;
using Api.Model.RequestModel.Music;
using Api.Model.ResponseModel.MicroController;
using Api.Model.ResponseModel.Music;
using Api.Services.MicroControllerServices;
using Api.Services.MusicPlayerToMicroControllerServices;
using Api.Services.StreamToByteArrayServices;

namespace Api.Services.MusicServices;

public interface IMusicServices
{
    Task<byte[]?> GetMusicInMinio(int id);
    Task<bool> Play(int idMusic, int[] idController);
    Task<bool> PlayLife(IFormFile formFile, int[] idController);
    Task<bool> Stop();
    Task<bool> CreateOrSave(string item, IFormFile formFile, string name);
    Task<List<DtoMusic>?> GetLimit(string item, int limit);
    Task<DtoMusic?> GetId(string item, int id);
    Task<List<DtoMusic>?> GetUni(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id, string path);
    Task<bool> UpdateId(string item, Music music, int id);
    Task<bool> Search(string item, string name);
}

public class MusicServices(
    IRepository<Music, DtoMusic> musicRepository,
    IAudioFileServices.IAudioMusicFileServices audioMusicFileServices,
    IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices,
    IMicroControllerServices microControllerServices,
    IStreamToByteArrayServices streamToByteArrayServices) : IMusicServices
{
    
    public async Task<byte[]?> GetMusicInMinio(int id)
    {
        DtoMusic? dtoMusic = await musicRepository.GetId("Musics", id);
        if (dtoMusic != null)
        {
            string name = dtoMusic.Name;
            Stream read = await audioMusicFileServices.GetStreamMusic(name);
            if (read != null)
                return await streamToByteArrayServices.StreamToByte(read);
        }

        return null;
    }
    
    public async Task<bool> Play(int idMusic, int[] idController)
    {
        foreach (var data in idController)
        {
            if (data < 0)
                continue;
            DtoMicroController? dtoMicroController = await microControllerServices.GetId("MicroControllers", data);
            if (dtoMicroController != null)
                await musicPlayerToMicroControllerServices.Play(
                    dtoMicroController, idMusic);
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
                    dtoMicroController, formFile);
        }

        return true;
    }

    public async Task<bool> Stop()
    {
        return await musicPlayerToMicroControllerServices.Stop();
    }

    public async Task<bool> CreateOrSave(string item, IFormFile formFile, string name)
    {
        Music? music = await audioMusicFileServices.SaveMusic(formFile, name);
        if (music != null)
            return await musicRepository.CreateOrSave(item, music);

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
        if (await audioMusicFileServices.DeleteMusic(path))
            return await musicRepository.DeleteId(item, id);

        return false;
    }

    public async Task<bool> UpdateId(string item, Music music, int id)
    {
        if (!await audioMusicFileServices.UpdateName(music.Name))
            return false;

        return await musicRepository.UpdateId(item, music, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await musicRepository.Search(item, name);
    }
}