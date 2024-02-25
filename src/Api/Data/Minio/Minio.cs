using System.Runtime.Intrinsics.X86;
using Api.Model.MinioModel;
using Api.Model.RequestModel.PlayList;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.DataModel.Encryption;
using Aes = System.Security.Cryptography.Aes;

namespace Api.Data.Minio;

public interface IMinio
{
    public Task<PlayList> Save(MinioModel minioModel);

    public Task<bool> Delete();
    public Task<string> Get();
    public Task<bool> Update();
}

public class Minio : IMinio
{
    private IMinioClient _minioClient;
    private IConfiguration _configuration;

    public Minio(IConfiguration configuration)
    {
        _configuration = configuration;
        _minioClient = new MinioClient()
            .WithEndpoint(_configuration.GetSection("Minio:url").Value)
            .WithCredentials(_configuration.GetSection("Minio:user").Value,
                _configuration.GetSection("Minio:pass").Value)
            .Build();
    }

    public async Task<PlayList> Save(MinioModel minioModel)
    {
        if (!await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(minioModel.BucketName)))
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(minioModel.BucketName));

        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(minioModel.BucketName)
            .WithObject(minioModel.FormFile.FileName)
            .WithStreamData(minioModel.FormFile.OpenReadStream())
            .WithObjectSize(minioModel.FormFile.OpenReadStream().Length)
            .WithContentType(minioModel.Type));

        return new PlayList(minioModel.Name, minioModel.Description, minioModel.FormFile.FileName);
    }

    public async Task<bool> Delete()
    {
        throw new NotImplementedException();
    }

    public async Task<string> Get()
    {
        return "";
    }

    public Task<bool> Update()
    {
        throw new NotImplementedException();
    }
}