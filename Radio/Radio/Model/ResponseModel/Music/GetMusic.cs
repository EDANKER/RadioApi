namespace Radio.Model.Music;

public class GetMusic
{
    public GetMusic(int id, string name, string path, int idPlayList)
    {
        Id = id;
        Name = name;
        Path = path;
        IdPlayList = idPlayList;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public int IdPlayList { get; set; }
}