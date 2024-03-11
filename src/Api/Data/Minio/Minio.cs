using Api.Model.MinioModel;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Api.Data.Minio;

public interface IMinio
{
    Task<bool> Save(MinioModel minioModel, IFormFile formFile, string type);
    Task<bool> Delete(MinioModel minioModel);
    Task<bool> Update(MinioModel minioModel, string newName, string type);
    Task<string> GetUrl(MinioModel minioModel);
    Task<Stream> GetByteMusic(MinioModel minioModel);
}

public class Minio(ILogger<Minio> logger, IConfiguration configuration) : IMinio
{
    private readonly IMinioClient _minioClient = new MinioClient()
        .WithEndpoint(configuration.GetSection("Minio:url").Value)
        .WithCredentials(configuration.GetSection("Minio:user").Value,
            configuration.GetSection("Minio:pass").Value)
        .Build();
    

    public async Task<bool> Save(MinioModel minioModel, IFormFile formFile, string type)
    {
        try
        {
            if (!await _minioClient.BucketExistsAsync(new BucketExistsArgs()
                    .WithBucket(minioModel.BucketName)))
                await _minioClient.MakeBucketAsync(new MakeBucketArgs()
                    .WithBucket(minioModel.BucketName));

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(minioModel.BucketName)
                .WithObject(formFile.FileName)
                .WithStreamData(formFile.OpenReadStream())
                .WithObjectSize(formFile.OpenReadStream().Length)
                .WithContentType(type));
            return true;
        }
        catch (MinioException e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> Delete(MinioModel minioModel)
    {
        try
        {
            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(minioModel.BucketName)
                .WithObject(minioModel.Name));

            return true;
        }
        catch (MinioException e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> Update(MinioModel minioModel, string newName, string type)
    {
        try
        {
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(minioModel.BucketName)
                .WithObject(newName)
                .WithStreamData(await GetByteMusic(minioModel))
                .WithObjectSize(GetByteMusic(minioModel).Result.Length)
                .WithContentType(type));
            await Delete(minioModel);
            
            return true;
        }
        catch (MinioException e)
        {
           logger.LogError(e.ToString());
           return false;
        }
    }

    public async Task<string> GetUrl(MinioModel minioModel)
    {
        TimeSpan timeSpan = TimeSpan.FromMinutes(5);
        try
        {
            return await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                .WithBucket(minioModel.BucketName)
                .WithObject(minioModel.Name)
                .WithExpiry((int)timeSpan.TotalSeconds));
        }
        catch (MinioException e)
        {
            logger.LogError(e.ToString());
            throw;
        }
    }

    public async Task<Stream> GetByteMusic(MinioModel minioModel)
    {
        Stream stream = new MemoryStream();
        try
        {
            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(minioModel.BucketName)
                .WithObject(minioModel.Name)
                .WithCallbackStream(stream.CopyTo));
            return stream;
        }
        catch (MinioException e)
        {
            logger.LogError(e.ToString());
            throw;
        }
    }
}