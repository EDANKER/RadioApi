namespace Api.DTO.PlayList;

public class DtoPlayList(int id, string name, string description, string imgPath)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string ImgPath { get; set; } = imgPath;
}