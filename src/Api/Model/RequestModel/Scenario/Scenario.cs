using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Scenario;

public class Scenario(int idMicroController, string time, string name, string days, int idMusic)
{
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public int IdMicroController { get; } = idMicroController;
    [Required]
    [StringLength(32, MinimumLength = 3, ErrorMessage = "< 32 > 3")]
    public string Time { get; } = time;
    [Required]
    [StringLength(203, MinimumLength = 3, ErrorMessage = "< 203 > 3")]
    public string Days { get; } = days;
    [Required]
    public int IdMusic { get; } = idMusic;
}