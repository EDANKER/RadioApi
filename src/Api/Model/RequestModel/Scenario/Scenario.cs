namespace Api.Model.RequestModel.Scenario;

public class Scenario(string sector, TimeSpan time, string name)
{
    public string Name { get; set; } = name;
    public string Sector { get; set; } = sector;
    public TimeSpan Time { get; set; } = time;
}