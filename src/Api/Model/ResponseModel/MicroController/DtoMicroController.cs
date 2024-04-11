namespace Api.Model.ResponseModel.MicroController;

public class DtoMicroController(int id, string name, string ip, int port, int floor, int cabinet)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Ip { get; set; } = ip;
    public int Port { get; set; } = port;
    public int Floor { get; set; } = floor;
    public int Cabinet { get; set; } = cabinet;
}