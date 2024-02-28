using Api.Data.Minio;
using Api.Data.Repository.Music;
using Api.Model.MinioModel;
using Api.Model.RequestModel.Music;
using Api.Model.ResponseModel.Music;
using Api.Services.AudioFileSaveToMicroControllerServices;
using Api.Services.MusicPlayerToMicroControllerServices;

namespace Api.Services.MusicServices;

public interface IMusicServices
{
    public Task<bool> Play(string path, List<string> florSector);
    public Task<bool> PlayLife(IFormFile formFile, List<string> florSector);
    public Task<bool> Stop();
    public Task<bool> CreateOrSave(string item, IFormFile formFile, string name);
    public Task<List<DtoMusic>> GetMusic(string item, int limit);
    public Task<DtoMusic> GetMusicId(string item, int id);
    public Task<List<DtoMusic>> GetMusicPlayListTag(string item, string name);
    public Task<bool> DeleteId(string item, int id, string path);
    public Task<bool> Update(string item, string field, string name, int id);
    public Task<bool> Search(string item, string name);
}

public class MusicServices(
    IMusicRepository musicRepository,
    IAudioFileSaveToMicroControllerServices audioFileSaveToMicroControllerServices,
    IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices,
    IMinio minio) : IMusicServices
{
    public async Task<bool> Play(string path, List<string> florSector)
    {
        return await musicPlayerToMicroControllerServices.PlayOne(
            await minio.GetByteMusic(new MinioModel(path, "music", "audio/mpeg")), florSector);
    }

    public async Task<bool> PlayLife(IFormFile formFile, List<string> florSector)
    {
        return await musicPlayerToMicroControllerServices.PlayOne(formFile.OpenReadStream(), florSector);
    }

    public async Task<bool> Stop()
    {
        return await musicPlayerToMicroControllerServices.Stop();
    }

    public async Task<bool> CreateOrSave(string item, IFormFile formFile, string name)
    {
        Music? music = await audioFileSaveToMicroControllerServices.SaveAudio(formFile, name);
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
        if (await audioFileSaveToMicroControllerServices.DeleteMusic(path))
            return await musicRepository.DeleteId(item, id);

        return false;
    }

    public async Task<bool> Update(string item, string field, string name, int id)
    {
        await audioFileSaveToMicroControllerServices.UpdateName(name);
        return await musicRepository.Update(item, field, name, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await musicRepository.Search(item, name);
    }
}