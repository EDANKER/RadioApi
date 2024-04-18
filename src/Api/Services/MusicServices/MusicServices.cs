using Api.Data.Repository.Music;
using Api.Model.RequestModel.Music;
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
    Task<List<DtoMusic>?> GetMusic(string item, int limit);
    Task<DtoMusic?> GetId(string item, int id);
    Task<List<DtoMusic>?> GetTag(string item, string name);
    Task<bool> DeleteId(string item, int id, string path);
    Task<bool> Update(string item, string path, string field, string name, int id);
    Task<bool> Search(string item, string name);
}

public class MusicServices(
    IMusicRepository musicRepository,
    IAudioFileServices.IAudioFileServices audioFileServices,
    IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices,
    IMicroControllerServices microControllerServices,
    IStreamToByteArrayServices streamToByteArrayServices) : IMusicServices
{
    
    public async Task<byte[]?> GetMusicInMinio(int id)
    {
        DtoMusic? dtoMusic = musicRepository.GetId("Musics", id).Result;
        if (dtoMusic != null)
        {
            string name = dtoMusic.Name;
            Stream read = audioFileServices.GetStreamMusic(name).Result;
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
            await musicPlayerToMicroControllerServices.Play(
                await microControllerServices.GetId("MicroControllers", data), idMusic);
        }

        return true;
    }

    public async Task<bool> PlayLife(IFormFile formFile, int[] idController)
    {
        foreach (var data in idController)
        {
            if (data < 0)
                continue;
            await musicPlayerToMicroControllerServices.PlayLife(
                await microControllerServices.GetId("MicroControllers", data), formFile);
        }

        return true;
    }

    public async Task<bool> Stop()
    {
        return await musicPlayerToMicroControllerServices.Stop();
    }

    public async Task<bool> CreateOrSave(string item, IFormFile formFile, string name)
    {
        Music? music = await audioFileServices.SaveAudio(formFile, name);
        if (music != null)
            return await musicRepository.CreateOrSave(item, music);

        return false;
    }

    public async Task<List<DtoMusic>?> GetMusic(string item, int limit)
    {
        return await musicRepository.GetLimit(item, limit);
    }

    public async Task<DtoMusic?> GetId(string item, int id)
    {
        return await musicRepository.GetId(item, id);
    }

    public async Task<List<DtoMusic>?> GetTag(string item, string name)
    {
        return await musicRepository.GetMusicPlayListTag(item, name);
    }

    public async Task<bool> DeleteId(string item, int id, string path)
    {
        if (await audioFileServices.DeleteMusic(path))
            return await musicRepository.DeleteId(item, id);

        return false;
    }

    public async Task<bool> Update(string item, string path, string field, string name, int id)
    {
        if (await audioFileServices.UpdateName(path, name))
            return await musicRepository.Update(item, field, name, id);

        return await musicRepository.Update(item, field, name, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await musicRepository.Search(item, name);
    }
}