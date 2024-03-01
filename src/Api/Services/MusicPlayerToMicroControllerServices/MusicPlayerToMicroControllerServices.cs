namespace Api.Services.MusicPlayerToMicroControllerServices;

public interface IMusicPlayerToMicroControllerServices
{
    public Task<bool> SoundVol(int vol);
    public Task<bool> PlayMore(Stream memoryStream, List<string> florSector);
    public Task<bool> PlayOne(Stream memoryStream, List<string> florSector);
    public Task<bool> Stop();
}

public class MusicPlayerToMicroControllerServices : IMusicPlayerToMicroControllerServices
{
    public async Task<bool> SoundVol(int vol)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PlayMore(Stream memoryStream, List<string> florSector)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PlayOne(Stream memoryStream, List<string> florSector)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Stop()
    {
        return true;
    }
}