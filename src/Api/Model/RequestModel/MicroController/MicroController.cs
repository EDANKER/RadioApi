using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.MicroController;

public class MicroController(string name, string ip, int port, int floor, int cabinet)
{
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Ip { get; } = ip;
    [Required]
    [Range(0, 64)]
    public int Port { get; } = port;
    [Required]
    [Range(0, 64)]
    public int Floor { get; } = floor;
    [Required]
    [Range(0, 64)]
    public int Cabinet { get; } = cabinet;
}