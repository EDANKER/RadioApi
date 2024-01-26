using Radio.Controller.Music;
using Radio.Model.Music;
using Radio.Model.User;

namespace Radio.Services.MusicServices;

public interface IMusicServices
{
    public List<Music> GetMusic(int limit);
}

public class MusicServices : IMusicServices
{
    private List<Music> _musics;

    public MusicServices(List<Music> musics)
    {
        _musics = musics;
    }

    public List<Music> GetMusic(int limit)
    {
        _musics = new List<Music>();


        return _musics;
    }
}