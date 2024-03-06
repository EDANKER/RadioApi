namespace Api.Model.RequestModel.Scenario;

public class Scenario(string sector, string time, string name)
{
    public string Name { get; set; } = name;
    public string Sector { get; set; } = sector;
    public string Time { get; set; } = time;
}