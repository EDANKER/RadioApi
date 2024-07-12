using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.PlayScenario;

public class DtoPlayScenario(int id, string name, int[] idMicroControllers, int idMusic)
{
    private int Id { get; set; } = id;
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Name { get; set; } = name;
    public int[] IdMicroControllers { get; set; } = idMicroControllers;
    [StringLength(32, MinimumLength = 3, ErrorMessage = "< 32 > 3")]
    public int IdMusic { get; set; } = idMusic;
}