namespace Api.Model.RequestModel.Update.UpdateMusic;

public class UpdateMusic(string name, string namePlayList)
{
    public string Name { get; set; } = name;
    public string NamePlayList { get; set; } = namePlayList;
}