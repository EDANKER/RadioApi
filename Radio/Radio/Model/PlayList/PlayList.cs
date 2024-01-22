namespace Radio.Model.User;

public class PlayList
{
    public PlayList(string name, string imgPath, Music[] musics)
    {
        Name = name;
        ImgPath = imgPath;
        Musics = musics;
    }

    public string Name { get; set; }
    public string ImgPath { get; set; }
    public Music[] Musics { get; set; }
}