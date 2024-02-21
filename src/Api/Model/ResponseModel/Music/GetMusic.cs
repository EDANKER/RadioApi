namespace Api.Model.ResponseModel.Music;

public class GetMusic(int id, string name, string path, string namePlayList)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Path { get; set; } = path;
    public string NamePLayList { get; set; } = namePlayList;
}