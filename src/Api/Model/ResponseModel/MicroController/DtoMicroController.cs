using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.MicroController;

public class DtoMicroController(int id, string name, string ip, int port, int floor, int cabinet)
{
    public int Id { get; set; } = id;
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Name { get; set; } = name;
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Ip { get; set; } = ip;
    [Range(0, 64, ErrorMessage = "< 64 > 3")]
    public int Port { get; set; } = port;
    [Range(0, 64, ErrorMessage = "< 64 > 3")]
    public int Floor { get; set; } = floor;
    [Range(0, 64, ErrorMessage = "< 64 > 3")]
    public int Cabinet { get; set; } = cabinet;
}