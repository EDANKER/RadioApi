namespace Api.Model.DTOs.MicroController;

public class DtoMicroController(string name, string ip)
{
    public string Name { get; set; } = name;
    public string Ip { get; set; } = ip;
}