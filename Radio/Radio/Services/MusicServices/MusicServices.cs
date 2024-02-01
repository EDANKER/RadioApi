using Radio.Data.Repository;
using Radio.Model.RequestModel.Music;

namespace Radio.Services.MusicServices;

public interface IMusicServices
{
    public Task<List<Music>> GetMusic(int limit);
    public Task<List<Music>> GetMusicId(int id);
    public Task<List<Music>> GetMusicTagPlayList(int id);
}

public class MusicServices : IMusicServices
{
    private IMusicRepository _repository;
    private List<Music> _musicsList;
    private Music _music;

    public MusicServices(List<Music> musicsList, IMusicRepository repository)
    {
        _musicsList = musicsList;
        _repository = repository;
    }

    public async Task<List<Music>> GetMusic(int limit)
    {
        _musicsList = new List<Music>();

        var reader = await _repository.GetLimit("Music", limit);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                object name = reader.GetValue(0);
                object path = reader.GetValue(1);
                object idPlayList = reader.GetValue(2);

                _music = new Music(name.ToString(), path.ToString(), idPlayList.ToString());
                _musicsList.Add(_music);
            }
        }
        
        return _musicsList;
    }

    public async Task<List<Music>> GetMusicId(int id)
    {
        _musicsList = new List<Music>();

        var reader = await _repository.GetId("Music", id);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                object name = reader.GetValue(0);
                object path = reader.GetValue(1);
                object idPlayList = reader.GetValue(2);

                _music = new Music(name.ToString(), path.ToString(), idPlayList.ToString());
                _musicsList.Add(_music);
            }
        }
        
        return _musicsList;
    }

    public async Task<List<Music>> GetMusicTagPlayList(int id)
    {
        _musicsList = new List<Music>();

        var reader = await _repository.GetPlayListTag("Music", id);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                object name = reader.GetValue(0);
                object path = reader.GetValue(1);
                object idPlayList = reader.GetValue(2);

                _music = new Music(name.ToString(), path.ToString(), idPlayList.ToString());
                _musicsList.Add(_music);
            }
        }
        
        return _musicsList;
    }
}