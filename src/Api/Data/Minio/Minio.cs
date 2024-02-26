using Api.Model.MinioModel;
using Api.Model.RequestModel.PlayList;
using Minio;
using Minio.DataModel.Args;

namespace Api.Data.Minio;

public interface IMinio
{
    public Task<bool> Save(MinioModel minioModel, IFormFile formFile);
    public Task<bool> Delete(MinioModel minioModel);
    public Task<string?> Get(MinioModel minioModel);
}

public class Minio(IConfiguration configuration) : IMinio
{
    private readonly IMinioClient _minioClient = new MinioClient()
        .WithEndpoint(configuration.GetSection("Minio:url").Value)
        .WithCredentials(configuration.GetSection("Minio:user").Value,
            configuration.GetSection("Minio:pass").Value)
        .Build();

    public async Task<bool> Save(MinioModel minioModel, IFormFile formFile)
    {
        try
        {
            if (!await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(minioModel.BucketName)))
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(minioModel.BucketName));

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(minioModel.BucketName)
                .WithObject(formFile.FileName)
                .WithStreamData(formFile.OpenReadStream())
                .WithObjectSize(formFile.OpenReadStream().Length)
                .WithContentType(minioModel.Type));
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Delete(MinioModel minioModel)
    {
        try
        {
            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(minioModel.BucketName)
                .WithObject(minioModel.Name));
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<string?> Get(MinioModel minioModel)
    {
        TimeSpan timeSpan = TimeSpan.FromMinutes(1);
        try
        {
            return await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs().WithBucket(minioModel.BucketName)
                .WithObject(minioModel.Name).WithExpiry((int)timeSpan.TotalSeconds));
        }
        catch (Exception e)
        {
            return null;
        }
    }
}