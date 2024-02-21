namespace Api.Services.MusicPlayerToMicroControllerServices;

public interface IMusicPlayerToMicroControllerServices
{
    public Task<bool> Play(string path, string[] florSector);
    public Task<bool> Stop();
}

public class MusicPlayerToMicroControllerServices : IMusicPlayerToMicroControllerServices
{
    public async Task<bool> Play(string path, string[] florSector)
    {
        return true;
    }

    public async Task<bool> Stop()
    {
        return true;
    }
}