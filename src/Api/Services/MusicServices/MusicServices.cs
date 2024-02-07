using Radio.Data.Repository;
using Radio.Model.Music;
using Radio.Model.RequestModel.Music;

namespace Api.Services.MusicServices;

public interface IMusicServices
{
    public Task<bool> CreateOrSave(string item, Music music);
    public Task<List<GetMusic>> GetMusic(string item,int limit);
    public Task<List<GetMusic>> GetMusicId(string item,int id);
    public Task<List<GetMusic>> GetMusicTagPlayList(string item,int id);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, string field, string name, int id);
    public Task<bool> Search(string item, string name);
}

public class MusicServices(IMusicRepository musicRepository) : IMusicServices
{
    public async Task<bool> CreateOrSave(string item, Music music)
    {
        return await musicRepository.CreateOrSave(item, music);
    }

    public async Task<List<GetMusic>> GetMusic(string item,int limit)
    {
        return await musicRepository.GetLimit(item, limit);
    }

    public async Task<List<GetMusic>> GetMusicId(string item, int id)
    {
        return await musicRepository.GetId(item, id);
    }

    public async Task<List<GetMusic>> GetMusicTagPlayList(string item, int id)
    {
        return await musicRepository.GetPlayListTag(item, id);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await musicRepository.DeleteId(item, id);
    }

    public async Task<bool> Update(string item, string field, string name, int id)
    {
        return await musicRepository.Update(item, field, name, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await musicRepository.Search(item, name);
    }
}