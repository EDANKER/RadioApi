using Radio.Data.Repository.PlayList;
using Radio.Model.PlayList;

namespace Api.Services.PlayListServices;

public interface IPlayListServices
{
    public Task<bool> CreateOrSave(string item, PlayList playList);
    public Task<List<GetPlayList>> GetPlayList(string item, int limit);
    public Task<List<GetPlayList>> GetPlayListId(string item,int id);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, string field, string name, int id);
    public Task<bool> Search(string item, string name);
}

public class PlayListServices(IPlayListRepository playListRepository) : IPlayListServices
{
    public async Task<bool> CreateOrSave(string item, PlayList playList)
    {
        return await playListRepository.CreateOrSave(item, playList);
    }

    public async Task<List<GetPlayList>> GetPlayList(string item,int limit)
    {
        return await playListRepository.GetLimit("PlayLists", limit);
    }

    public async Task<List<GetPlayList>> GetPlayListId(string item,int id)
    {
        return await playListRepository.GetId("PlayLists", id);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await playListRepository.DeleteId(item, id);
    }

    public async Task<bool> Update(string item, string field, string name, int id)
    {
        return await playListRepository.Update(item, name, field, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await playListRepository.Search(item, name);
    }
}