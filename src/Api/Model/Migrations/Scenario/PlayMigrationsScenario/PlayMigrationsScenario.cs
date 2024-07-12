using System.ComponentModel.DataAnnotations;

namespace Api.Model.Migrations.Scenario.PlayMigrationsScenario;

public class PlayMigrationsScenario(int id, string name, string idMicroControllers, int idMusic)
{
    public int Id { get; set; } = id;
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Name { get; set; } = name;
    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string IdMicroControllers { get; set; } = idMicroControllers;
    [Range(0, 64, ErrorMessage = "< 64 > 3")]
    public int IdMusic { get; set; } = idMusic;
}