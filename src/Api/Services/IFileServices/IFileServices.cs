using Api.Data.Minio;
using Api.Model.MinioModel;

namespace Api.Services.IFileServices;

public interface IFileServices
{
    Task<bool> Save(IFormFile formFile, string name, string bucketName, string type);
    Task<bool> Delete(string path, string bucketName);
    Task<bool> UpdateName(string oldName, string newName, string bucketName, string type);
    Task<string> GetUrl(string imgPath, string bucketName);
    Task<Stream?> GetStream(string path, string bucketName);
}

public class FileServices(
    IMinio minio)
    : IFileServices
{
    public async Task<bool> Save(IFormFile formFile, string name, string bucketName, string type)
    {
        if (await minio.Save(new MinioModel(name,
                bucketName), formFile, type))
            return true;

        return false;
    }
    
    public async Task<bool> Delete(string path, string bucketName)
    {
        return await minio.Delete(new MinioModel(path, bucketName));
    }

    public async Task<bool> UpdateName(string oldName, string name, string bucketName, string type)
    {
        return await minio.UpdateName(new MinioModel(oldName, bucketName), name, "audio/mpeg");
    }

    public async Task<string> GetUrl(string imgPath, string bucketName)
    {
        return await minio.GetUrl(new MinioModel(imgPath, bucketName));
    }

    public async Task<Stream?> GetStream(string path, string bucketName)
    {
        return await minio.GetByteMusic(new MinioModel(path, bucketName));
    }
}