using System.ComponentModel.DataAnnotations;

namespace Api.Model.MinioModel;

public class MinioModel(
    string name,
    string bucketName)
{
    [Required] public string Name { get; } = name;
    [Required] public string BucketName { get; } = bucketName;
}