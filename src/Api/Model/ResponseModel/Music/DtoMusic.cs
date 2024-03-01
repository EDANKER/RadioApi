namespace Api.Model.ResponseModel.Music;

public class DtoMusic(int id, string name, string namePlayList, string timeMusic)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string NamePLayList { get; set; } = namePlayList;
    public string TimeMusic { get; set; } = timeMusic;
}