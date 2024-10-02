using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.TimeScenario;

public class DtoTimeScenario(int id, string name, int[] idMicroControllers, string time, string days, int idMusic)
{
    [Required] public int Id { get; set; } = id;

    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; set; } = name;

    [Required] public int[] IdMicroControllers { get; set; } = idMicroControllers;

    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Time { get; set; } = time;

    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Days { get; set; } = days;

    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public int IdMusic { get; set; } = idMusic;
}