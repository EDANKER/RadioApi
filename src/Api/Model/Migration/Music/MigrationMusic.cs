namespace Api.Model.Migration.Music;

public class MigrationMusic(int id, string name, string path, string namePlayList, string timeMusic)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Path { get; set; } = path;
    public string NamePLayList { get; set; } = namePlayList;
    public string TimeMusic { get; set; } = timeMusic;
}