using Api.Model.RequestModel.Music;
using Api.Services.MusicPlayerToMicroControllerServices;
using NAudio.Wave;

namespace Api.Services.AudioFileSaveToMicroControllerServices;

public interface IAudioFileSaveToMicroControllerServices
{
    public Task<Music> SaveAudio(IFormFile formFile, string name);
    public Task<bool> SaveThenPlay(IFormFile formFile, string[] florSector);
    public Task<bool> DeleteMusic(string path);
    public Task<bool> UpdateName(string name);
}

public class AudioFileSaveToMicroControllerServices(
    IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices)
    : IAudioFileSaveToMicroControllerServices
{
    public async Task<Music> SaveAudio(IFormFile formFile, string name)
    {
        if (!await Save(formFile)) return default;
        return new Music(formFile.FileName, $"Data/Uploads/Music/{formFile.FileName}", name, TimeMusic($"Data/Uploads/Music/{formFile.FileName}"));
    }

    public async Task<bool> SaveThenPlay(IFormFile formFile, string[] florSector)
    {
        if (!await Save(formFile)) return false;
        return await musicPlayerToMicroControllerServices.Play($"Data/Uploads/Music/{formFile.FileName}", florSector);
    }

    public async Task<bool> DeleteMusic(string path)
    {
        return true;
    }

    public async Task<bool> UpdateName(string name)
    {
        return true;
    }

    private static TimeSpan TimeMusic(string path)
    {
        Mp3FileReader mp3FileReader = new Mp3FileReader(path);
        return mp3FileReader.TotalTime;
    }

    private static async Task<bool> Save(IFormFile formFile)
    {
        string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Uploads/Music");
        string filePath = Path.Combine(uploadsPath, formFile.FileName);
        try
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);
            fileStream.Close();
        }
        catch (Exception)
        {
            return false;
        }
    
        return true;
    }
}