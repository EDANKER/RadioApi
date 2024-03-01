namespace Api.Model.Migration.Music;

public class MigrationMusic(int id, string name, string namePLayList, string timeMusic)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string NamePLayList { get; set; } = namePLayList;
    public string TimeMusic { get; set; } = timeMusic;
}