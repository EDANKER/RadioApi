using Radio.Data.Repository;
using Radio.Model.Music;
using Radio.Model.RequestModel.Music;

namespace Radio.Services.MusicServices;

public interface IMusicServices
{
    public Task<List<GetMusic>> GetMusic(string item,int limit);
    public Task<List<GetMusic>> GetMusicId(string item,int id);
    public Task<List<GetMusic>> GetMusicTagPlayList(string item,int id);
}

public class MusicServices : IMusicServices
{
    private IMusicRepository _repository;

    public MusicServices(IMusicRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetMusic>> GetMusic(string item,int limit)
    {
        return await _repository.GetLimit(item, limit);
    }

    public async Task<List<GetMusic>> GetMusicId(string item, int id)
    {
        return await _repository.GetId(item, id);
    }

    public async Task<List<GetMusic>> GetMusicTagPlayList(string item, int id)
    {
        return await _repository.GetPlayListTag(item, id);
    }
}