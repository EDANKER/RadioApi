using System.ComponentModel.DataAnnotations;

namespace Api.Model.Migrations.Scenario.TimeMigrationsScenario;

public class TimeMigrationsScenario(
    int id,
    string name,
    string idMicroControllers,
    string time,
    string days,
    int idMusic)
{
    public int Id { get; set; } = id;

    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; set; } = name;

    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string IdMicroControllers { get; set; } = idMicroControllers;

    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Time { get; set; } = time;

    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Days { get; set; } = days;

    public int IdMusic { get; set; } = idMusic;
}