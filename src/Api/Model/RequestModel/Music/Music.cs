namespace Radio.Model.RequestModel.Music;

public class Music
{
    public Music(string name, string path, int idPlayList)
    {
        Name = name;
        Path = path;
        IdPlayList = idPlayList;
    }

    public string Name { get; set; }
    public string Path { get; set; }
    public int IdPlayList { get; set; }
}