using Api.Model.RequestModel.Music;
using NAudio.Wave;
using Api.Data.Minio;
using Api.Model.MinioModel;

namespace Api.Services.AudioFileSaveToMicroControllerServices;

public interface IAudioFileSaveToMicroControllerServices
{
    public Task<Music?> SaveAudio(IFormFile formFile, string name);
    public Task<bool> DeleteMusic(string path);
    public Task<bool> UpdateName(string name);
}

public class AudioFileSaveToMicroControllerServices(
    IMinio minio)
    : IAudioFileSaveToMicroControllerServices
{
    public async Task<Music?> SaveAudio(IFormFile formFile, string name)
    {
        if (await Save(formFile))
            return new Music(formFile.FileName, formFile.FileName, name,
                TimeMusic(formFile));

        return null;
    }
    
    public async Task<bool> DeleteMusic(string path)
    {
        return await minio.Delete(new MinioModel(path, "music", ""));
    }

    public async Task<bool> UpdateName(string path)
    {
        return await minio.Update(new MinioModel(path, "music", ""));
    }

    private static TimeSpan TimeMusic(IFormFile formFile)
    {
        Mp3FileReader mp3FileReader = new Mp3FileReader(formFile.OpenReadStream());
        return mp3FileReader.TotalTime;
    }

    private async Task<bool> Save(IFormFile formFile)
    {
        return await minio.Save(new MinioModel(formFile.FileName,
            "music", formFile.ContentType), formFile);
    }
}