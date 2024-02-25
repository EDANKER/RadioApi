namespace Api.Model.MinioModel;

public class MinioModel(
    IFormFile formFile,
    string name,
    string description,
    string bucketName,
    string type)
{
    public IFormFile FormFile { get; set; } = formFile;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string BucketName { get; set; } = bucketName;
    public string Type { get; set; } = type;
}