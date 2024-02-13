namespace Api.Model.RequestModel.PlayList;

public class PlayList(string name, string description, string imgPath)
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string ImgPath { get; set; } = imgPath;
}