
namespace Api.Model.RequestModel.Music;

public class Music(string name, string path, string namePlayList, TimeSpan timeMusic)
{
    public string Name { get; set; } = name;
    public string Path { get; set; } = path;
    public string NamePlayList { get; set; } = namePlayList;
    public TimeSpan TimeMusic {get; set;} = timeMusic;
}