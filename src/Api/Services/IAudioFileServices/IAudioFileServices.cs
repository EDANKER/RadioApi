using Api.Data.Minio;
using Api.Model.MinioModel;
using Api.Model.RequestModel.Music;
using TagLib;

namespace Api.Services.IAudioFileServices;

public interface IAudioFileServices
{
    Task<Music?> SaveAudio(IFormFile formFile, string name);
    Task<bool> DeleteMusic(string path);
    Task<bool> UpdateName(string path, string newName);
    Task<Stream> GetByteMusic(string path);
}

public class AudioFileServices(
    IMinio minio)
    : IAudioFileServices
{
    public async Task<Music?> SaveAudio(IFormFile formFile, string name)
    {
        if (await Save(formFile))
            return new Music(formFile.FileName, name,
                TimeMusic(formFile));

        return null;
    }

    public async Task<bool> DeleteMusic(string path)
    {
        return await minio.Delete(new MinioModel(path, "music"));
    }

    public async Task<bool> UpdateName(string path, string name)
    {
        return await minio.Update(new MinioModel(path, "music"), name, "audio/mpeg");
    }

    public async Task<Stream> GetByteMusic(string path)
    {
        return await minio.GetByteMusic(new MinioModel(path, "music"));
    }

    private static double TimeMusic(IFormFile formFile)
    {
        TagLib.File bit = TagLib.File.Create(new StreamFileAbstraction(formFile.FileName, formFile.OpenReadStream(),
            formFile.OpenReadStream()));
        return bit.Properties.Duration.TotalSeconds;
    }

    private async Task<bool> Save(IFormFile formFile)
    {
        return await minio.Save(new MinioModel(formFile.FileName,
            "music"), formFile, "audio/mpeg");
    }
}