using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Create.CreateMusic;

public class CreateMusic(string name, string namePlayList, int timeMusic)
{
    [Required]
    [StringLength(203, MinimumLength = 3, ErrorMessage = "< 203 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "< 120 > 3")]
    public string NamePlayList { get; } = namePlayList;
    [Required]
    [StringLength(32, MinimumLength = 3, ErrorMessage = "< 32 > 3")]
    public int TimeMusic {get; } = timeMusic;
}