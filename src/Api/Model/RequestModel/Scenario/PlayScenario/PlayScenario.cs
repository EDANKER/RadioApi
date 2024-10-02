using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Scenario.PlayScenario;

public class PlayScenario(string name, int[] idMicroController, int idMusic)
{
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; } = name;
    [Required]
    public int[] IdMicroController { get; } = idMicroController;
    [Required]
    public int IdMusic { get; } = idMusic;
}