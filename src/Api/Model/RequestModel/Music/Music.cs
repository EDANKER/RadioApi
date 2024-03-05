
namespace Api.Model.RequestModel.Music;

public class Music(string name, string path, string namePlayList, double timeMusic)
{
    public string Name { get; set; } = name;
    public string Path { get; set; } = path;
    public string NamePlayList { get; set; } = namePlayList;
    public double TimeMusic {get; set;} = timeMusic;
}