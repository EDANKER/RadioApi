namespace Api.Model.Migrations.MigScenario;

public class MigScenario(int id, string name, string idMicroControllers, string time, string days, int idMusic)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string IdMicroControllers { get; set; } = idMicroControllers;
    public string Time { get; set; } = time;
    public string Days { get; set; } = days;
    public int IdMusic { get; set; } = idMusic;
}