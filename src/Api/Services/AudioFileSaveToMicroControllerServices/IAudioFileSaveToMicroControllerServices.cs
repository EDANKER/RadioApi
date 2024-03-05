﻿using Api.Model.RequestModel.Music;
using Api.Data.Minio;
using Api.Model.MinioModel;
using TagLib;

namespace Api.Services.AudioFileSaveToMicroControllerServices;

public interface IAudioFileSaveToMicroControllerServices
{
    Task<Music?> SaveAudio(IFormFile formFile, string name);
    Task<bool> DeleteMusic(string path);
    Task<bool> UpdateName(string name);
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

    private static double TimeMusic(IFormFile formFile)
    {
        TagLib.File file = TagLib.File.Create(new StreamFileAbstraction(formFile.FileName, formFile.OpenReadStream(),
            formFile.OpenReadStream()));
        return file.Properties.Duration.TotalSeconds;
    }

    private async Task<bool> Save(IFormFile formFile)
    {
        return await minio.Save(new MinioModel(formFile.FileName,
            "music", formFile.ContentType), formFile);
    }
}