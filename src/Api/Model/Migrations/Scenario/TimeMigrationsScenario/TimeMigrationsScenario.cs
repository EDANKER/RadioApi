using System.ComponentModel.DataAnnotations;

namespace Api.Model.Migrations.Scenario.TimeMigrationsScenario;

public class TimeMigrationsScenario(int id, string name, string idMicroControllers, string time, string days, int idMusic)
{
    public int Id { get; set; } = id;
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Name { get; set; } = name;
    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string IdMicroControllers { get; set; } = idMicroControllers;
    [StringLength(32, MinimumLength = 3, ErrorMessage = "< 32 > 3")]
    public string Time { get; set; } = time;
    [StringLength(203, MinimumLength = 3, ErrorMessage = "< 203 > 3")]
    public string Days { get; set; } = days;
    [Range(0, 64, ErrorMessage = "< 64 > 3")]
    public int IdMusic { get; set; } = idMusic;
}