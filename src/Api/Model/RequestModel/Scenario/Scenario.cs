namespace Api.Model.RequestModel.Scenario;

public class Scenario(string sector, DateTime time, string name)
{
    public string Name { get; set; } = name;
    public string Sector { get; set; } = sector;
    public DateTime Time { get; set; } = time;
}