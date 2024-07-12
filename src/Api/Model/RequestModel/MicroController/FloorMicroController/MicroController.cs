using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.MicroController.FloorMicroController;

public class MicroController(string name, string ip, int port, string place)
{
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Ip { get; } = ip;
    [Required]
    public int Port { get; } = port;
    [Required]
    [Range(0, 64)]
    public string Place { get; } = place;
}