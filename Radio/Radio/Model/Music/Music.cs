namespace Radio.Model.User;

public class Music
{
    public Music(string name, string path, string namePlayList)
    {
        Name = name;
        Path = path;
        NamePlayList = namePlayList;
    }

    public string Name { get; set; }
    public string Path { get; set; }
    public string NamePlayList { get; set; }
}