using Api.Data.Repository.Music;
using Api.Model.RequestModel.Music;
using Api.Model.ResponseModel.Music;
using Api.Services.MicroControllerServices;
using Api.Services.MusicPlayerToMicroControllerServices;

namespace Api.Services.MusicServices;

public interface IMusicServices
{
    Task<bool> Play(int idMusic, int idController);
    Task<bool> PlayLife(IFormFile formFile, string[] florSector);
    Task<bool> Stop();
    Task<bool> CreateOrSave(string item, IFormFile formFile, string name);
    Task<List<DtoMusic>> GetMusic(string item, int limit);
    Task<DtoMusic> GetMusicId(string item, int id);
    Task<List<DtoMusic>> GetMusicPlayListTag(string item, string name);
    Task<bool> DeleteId(string item, int id, string path);
    Task<bool> Update(string item, string path, string field, string name, int id);
    Task<bool> Search(string item, string name);
}

public class MusicServices(
    IMusicRepository musicRepository,
    IAudioFileServices.IAudioFileServices audioFileServices,
    IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices,
    IMicroControllerServices microControllerServices) : IMusicServices
{
    public async Task<bool> Play(int idMusic, int idController)
    {
        return await musicPlayerToMicroControllerServices.Play(await microControllerServices.GetId("MicroControllers", idController), idMusic);
    }

    public async Task<bool> PlayLife(IFormFile formFile, string[] florSector)
    {
        return false;
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

    public async Task<List<DtoMusic>> GetMusic(string item, int limit)
    {
        return await musicRepository.GetLimit(item, limit);
    }

    public async Task<DtoMusic> GetMusicId(string item, int id)
    {
        return await musicRepository.GetId(item, id);
    }

    public async Task<List<DtoMusic>> GetMusicPlayListTag(string item, string name)
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