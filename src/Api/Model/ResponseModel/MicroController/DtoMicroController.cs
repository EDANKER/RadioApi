using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.MicroController;

public class DtoMicroController(int id, string name, string ip, int port, string place)
{
    [Required] public int Id { get; set; } = id;

    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; set; } = name;

    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    [Required]
    public string Ip { get; set; } = ip;

    [Required] public int Port { get; set; } = port;

    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Place { get; set; } = place;
}