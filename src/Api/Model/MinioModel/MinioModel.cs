namespace Api.Model.MinioModel;

public class MinioModel(
    string name,
    string bucketName)
{
    public string Name { get; set; } = name;
    public string BucketName { get; set; } = bucketName;
}