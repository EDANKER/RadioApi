namespace Api.Model.ResponseModel.Scenario;

public class DtoScenario(int id, string name, string idMicroController, string time, string days, int idMusic)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string IdMicroController { get; set; } = idMicroController;
    public string Time { get; set; } = time;
    public string Days { get; set; } = days;
    public int IdMusic { get; set; } = idMusic;
}