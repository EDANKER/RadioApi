namespace Api.Model.ResponseModel.MicroController;

public class GetMicroController(int id, string name, string ip)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Ip { get; set; } = ip;
}