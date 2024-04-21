namespace Api.Model.RequestModel.Scenario;

public class Scenario(int idMicroController, string time, string name, string days, int idMusic)
{
    public string Name { get; set; } = name;
    public int IdMicroController { get; set; } = idMicroController;
    public string Time { get; set; } = time;
    public string Days { get; set; } = days;
    public int IdMusic { get; set; } = idMusic;
}