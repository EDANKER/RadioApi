namespace Api.Model.ResponseModel.Scenario;

public class DtoScenario(int id, string name, int[] idMicroController, string time, string days, int idMusic)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public int[] IdMicroController { get; set; } = idMicroController;
    public string Time { get; set; } = time;
    public string Days { get; set; } = days;
    public int IdMusic { get; set; } = idMusic;
}