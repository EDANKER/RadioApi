namespace Api.Model.RequestModel.Music;

public class Music(string name, string path, int idPlayList)
{
    public string Name { get; set; } = name;
    public string Path { get; set; } = path;
    public int IdPlayList { get; set; } = idPlayList;
}