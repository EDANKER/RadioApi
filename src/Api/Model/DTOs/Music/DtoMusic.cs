namespace Api.DTO.Music;

public class DtoMusic(int id, string name, string path, int idPlayList)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Path { get; set; } = path;
    public int IdPlayList { get; set; } = idPlayList;
}