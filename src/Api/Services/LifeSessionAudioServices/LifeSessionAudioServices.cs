using Api.Services.SaveAudioFile;

namespace Api.Services.LifeSessionAudioServices;

public interface ILifeSessionAudioServices
{
    public Task<bool> Start(IFormFile formFile);
}

public class LifeSessionAudioServices(ISaveAudioFileServices saveAudioFileServices) : ILifeSessionAudioServices
{
    public async Task<bool> Start(IFormFile formFile)
    {
        return await saveAudioFileServices.SaveThenPlay(formFile);
    }
}