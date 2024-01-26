namespace Radio.Model.Music;

public class Music
{
    public Music(string name, string path, string idPlayList)
    {
        Name = name;
        Path = path;
        IdPlayList = idPlayList;
    }

    public string Name { get; set; }
    public string Path { get; set; }
    public string IdPlayList { get; set; }
}