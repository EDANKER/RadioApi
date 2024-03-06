namespace Api.Model.ResponseModel.Scenario;

public class DtoScenario(int id, string name, string sector, string time)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Sector { get; set; } = sector;
    public string Time { get; set; } = time;
}