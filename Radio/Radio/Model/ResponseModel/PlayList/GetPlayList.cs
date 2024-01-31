namespace Radio.Model.PlayList;

public class GetPlayList
{
    public GetPlayList(int id,string name, string description, string imgPath)
    {
        Id = id;
        Name = name;
        Description = description;
        ImgPath = imgPath;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImgPath { get; set; }
}