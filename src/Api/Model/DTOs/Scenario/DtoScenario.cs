namespace Api.Model.DTOs.Scenario;

public class DtoScenario(string sector, DateTime time, string name)
{
    public string Name { get; set; } = name;
    public string Sector { get; set; } = sector;
    public DateTime Time { get; set; } = time;
}