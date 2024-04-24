
namespace Api.Model.RequestModel.Music;

public class CreateMusic(string name, string namePlayList, double timeMusic)
{
    public string Name { get; set; } = name;
    public string NamePlayList { get; set; } = namePlayList;
    public double TimeMusic {get; set;} = timeMusic;
}