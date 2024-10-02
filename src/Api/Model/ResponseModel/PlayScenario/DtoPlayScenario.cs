using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.PlayScenario;

public class DtoPlayScenario(int id, string name, int[] idMicroControllers, int idMusic)
{
    [Required]
    private int Id { get; set; } = id;
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; set; } = name;
    public int[] IdMicroControllers { get; set; } = idMicroControllers;
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public int IdMusic { get; set; } = idMusic;
}