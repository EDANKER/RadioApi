using Api.Model.RequestModel.Music;
using Api.Services.TransmissionToMicroController;

namespace Api.Services.SaveAudioFile;

public interface ISaveAudioFileServices
{
    public Task<Music> SaveAudio(IFormFile formFile, int id);
    public Task<bool> SaveThenPlay(IFormFile formFile);
    public Task<bool> DeleteMusic(string path);
    public Task<bool> UpdateName(string name);
}

public class SaveAudioFileServicesServices(ITransmissionToMicroController transmissionToMicroController)
    : ISaveAudioFileServices
{
    public async Task<Music> SaveAudio(IFormFile formFile, int id)
    {
        await Save(formFile);
        return new Music(formFile.FileName, "Data/Uploads/Music/" + formFile.FileName, id);
    }

    public async Task<bool> SaveThenPlay(IFormFile formFile)
    {
        await Save(formFile);
        return await transmissionToMicroController.Transmission(formFile);
    }

    public Task<bool> DeleteMusic(string path)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateName(string name)
    {
        throw new NotImplementedException();
    }

    private static async Task Save(IFormFile formFile)
    {
        string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Uploads/Music");
        string filePath = Path.Combine(uploadsPath, formFile.FileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await formFile.CopyToAsync(fileStream);
    }
}