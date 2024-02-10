namespace Api.Model.Migration.Scenario;

public class MigrationScenario(int id, string sector, string time)
{
    public int Id { get; set; } = id;
    public string Sector { get; set; } = sector;
    public string Time { get; set; } = time;
}