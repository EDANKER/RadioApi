namespace Api.Model.RequestModel.MicroController;

public class MicroController(string name, string ip)
{
    public string Name { get; set; } = name;
    public string Ip { get; set; } = ip;
}