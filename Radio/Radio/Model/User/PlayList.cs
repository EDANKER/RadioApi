namespace Radio.Model.User;

public class PlayList
{
    public PlayList(string name, string imgPath)
    {
        Name = name;
        ImgPath = imgPath;
    }

    public string Name { get; set; }
    public string ImgPath { get; set; }
}