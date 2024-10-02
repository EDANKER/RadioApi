using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.Music;

public class DtoMusic(int id, string name, string namePLayList, int timeMusic)
{
    [Required] public int Id { get; set; } = id;

    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 203 > 3")]
    public string Name { get; set; } = name;

    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string NamePLayList { get; set; } = namePLayList;

    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public int TimeMusic { get; set; } = timeMusic;
}