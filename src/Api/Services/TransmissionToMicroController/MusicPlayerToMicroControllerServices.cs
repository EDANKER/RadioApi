namespace Api.Services.TransmissionToMicroController;

public interface IMusicPlayerToMicroControllerServices
{
    public Task<bool> Play(string path);
    public Task<bool> Stop();
}

public class MusicPlayerToMicroControllerServices : IMusicPlayerToMicroControllerServices
{
    public async Task<bool> Play(string path)
    {
        return true;
    }

    public async Task<bool> Stop()
    {
        return true;
    }
}