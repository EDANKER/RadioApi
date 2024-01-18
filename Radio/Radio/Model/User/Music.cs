namespace Radio.Model.User;

public class Music
{
    public Music(string name, string path, string namePlayList, string imgName)
    {
        Name = name;
        Path = path;
        NamePlayList = namePlayList;
        ImgName = imgName;
    }

    public string Name { get; set; }
    public string Path { get; set; }
    public string NamePlayList { get; set; }
    public string ImgName { get; set; }
}