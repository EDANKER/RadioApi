namespace Api.Model.RequestModel.MicroController;

public class MicroController(string name, string ip, int port, int floor, int cabinet)
{
    public string Name { get; set; } = name;
    public string Ip { get; set; } = ip;
    public int Port { get; set; } = port;
    public int Floor { get; set; } = floor;
    public int Cabinet { get; set; } = cabinet;
}