using Radio.Data.Repository;
using Radio.Model.RequestModel.Music;

namespace Radio.Services.MusicServices;

public interface IMusicServices
{
    public Task<List<Music>> GetMusic(int limit);
    public Task<List<Music>> GetMusicName(string name);
    public Task<List<Music>> GetMusicTagPlayList(int id);
}

public class MusicServices : IMusicServices
{
    private IMusicRepository _repository;
    private List<Music> _musics;
    private Music _music;

    public MusicServices(List<Music> musics, IMusicRepository repository)
    {
        _musics = musics;
        _repository = repository;
    }

    public async Task<List<Music>> GetMusic(int limit)
    {
        _musics = new List<Music>();

        var reader = await _repository.GetLimit("Music", limit);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                object name = reader.GetValue(0);
                object path = reader.GetValue(1);
                object idPlayList = reader.GetValue(2);

                _music = new Music(name.ToString(), path.ToString(), idPlayList.ToString());
            }
        }
        
        return _musics;
    }

    public async Task<List<Music>> GetMusicName(string nameTable)
    {
        _musics = new List<Music>();

        var reader = await _repository.GetName("Music", nameTable);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                object name = reader.GetValue(0);
                object path = reader.GetValue(1);
                object idPlayList = reader.GetValue(2);

                _music = new Music(name.ToString(), path.ToString(), idPlayList.ToString());
            }
        }
        
        return _musics;
    }

    public async Task<List<Music>> GetMusicTagPlayList(int id)
    {
        _musics = new List<Music>();

        var reader = await _repository.GetPlayListTag("Music", id);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                object name = reader.GetValue(0);
                object path = reader.GetValue(1);
                object idPlayList = reader.GetValue(2);

                _music = new Music(name.ToString(), path.ToString(), idPlayList.ToString());
            }
        }
        
        return _musics;
    }
}