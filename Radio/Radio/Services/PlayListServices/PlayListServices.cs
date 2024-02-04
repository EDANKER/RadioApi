using System.Data.Common;
using Radio.Data.Repository;
using Radio.Data.Repository.PlayList;
using Radio.Model.Music;
using Radio.Model.PlayList;
using Radio.Model.User;

namespace Radio.Services.PlayListServices;

public interface IPlayListServices
{
    public Task<List<GetPlayList>> GetPlayList(int limit);
    public Task<List<GetPlayList>> GetPlayListId(int id);
}

public class PlayListServices : IPlayListServices
{
    private IPlayListRepository _repository;

    public PlayListServices(IPlayListRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetPlayList>> GetPlayList(int limit)
    {
        return await _repository.GetLimit("PlayList", limit);
    }

    public async Task<List<GetPlayList>> GetPlayListId(int id)
    {
        return await _repository.GetId("PlayList", id);
    }
}