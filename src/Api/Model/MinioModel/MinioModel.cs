namespace Api.Model.MinioModel;

public class MinioModel(
    string name,
    string bucketName,
    string type)
{
    public string Name { get; set; } = name;
    public string BucketName { get; set; } = bucketName;
    public string Type { get; set; } = type;
}