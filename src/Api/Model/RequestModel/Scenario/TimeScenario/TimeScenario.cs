using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Scenario.TimeScenario;

public class TimeScenario(string name, int[]? idMicroController, string time, string[] days, int idMusic)
{
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Name { get; } = name;
    [Required]
    public int[]? IdMicroController { get; } = idMicroController;
    [Required]
    [StringLength(32, MinimumLength = 3, ErrorMessage = "< 32 > 3")]
    public string Time { get; } = time;
    [Required]
    public string[] Days { get; } = days;
    [Required]
    public int IdMusic { get; } = idMusic;
}