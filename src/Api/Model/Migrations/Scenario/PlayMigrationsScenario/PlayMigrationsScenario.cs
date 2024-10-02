using System.ComponentModel.DataAnnotations;

namespace Api.Model.Migrations.Scenario.PlayMigrationsScenario;

public class PlayMigrationsScenario(
    int id, 
    string name, 
    string idMicroControllers, 
    int idMusic)
{
    public int Id { get; set; } = id;

    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; set; } = name;

    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string IdMicroControllers { get; set; } = idMicroControllers;

    public int IdMusic { get; set; } = idMusic;
}