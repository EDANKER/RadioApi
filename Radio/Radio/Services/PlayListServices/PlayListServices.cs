using System.Data.Common;
using Radio.Data.Repository;
using Radio.Model.Music;
using Radio.Model.PlayList;
using Radio.Model.User;

namespace Radio.Services.PlayListServices;

public interface IPlayListServices
{
    public Task<List<PlayList>> GetPlayList(int limit);
    public Task<List<PlayList>> GetPlayListId(int id);
}

public class PlayListServices : IPlayListServices
{
    private IPlayListRepository _repository;
    private PlayList _playList;
    private List<PlayList> _playLists;

    public PlayListServices(IPlayListRepository repository, PlayList playList, List<PlayList> playLists)
    {
        _repository = repository;
        _playList = playList;
        _playLists = playLists;
    }

    public async Task<List<PlayList>> GetPlayList(int limit)
    {
        _playLists = new List<PlayList>();

        await _repository.GetLimit("PlayList", limit);

        var reader = await _repository.GetLimit("PlayList", limit);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                object name = reader.GetValue(0);
                object description = reader.GetValue(1);
                object imgPath = reader.GetValue(2);

                _playList = new PlayList(name.ToString(), description.ToString(), imgPath.ToString());
                _playLists.Add(_playList);
            }
        }

        return _playLists;
    }

    public async Task<List<PlayList>> GetPlayListId(int id)
    {
        _playLists = new List<PlayList>();
        DbDataReader reader = await _repository.GetId("PlayList", id);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                object name = reader.GetValue(0);
                object description = reader.GetValue(1);
                object imgPath = reader.GetValue(2);

                _playList = new PlayList(name.ToString(), description.ToString(), imgPath.ToString());
                _playLists.Add(_playList);
            }
        }

        return _playLists;
    }
}