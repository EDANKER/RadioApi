using Api.Services.AudioFileSaveToMicroControllerServices;

namespace Api.Services.LifeSessionAudioServices;

public interface ILifeSessionAudioServices
{
    public Task<bool> Start(IFormFile formFile);
}

public class LifeSessionAudioServices(IAudioFileSaveToMicroControllerServices audioFileSaveToMicroControllerServices) : ILifeSessionAudioServices
{
    public async Task<bool> Start(IFormFile formFile)
    {
        return await audioFileSaveToMicroControllerServices.SaveThenPlay(formFile);
    }
}