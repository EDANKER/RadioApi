using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Create.CreateMusic;

public class CreateMusic(string name, string namePlayList, int timeMusic)
{
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string NamePlayList { get; } = namePlayList;
    [Required]
    public int TimeMusic {get; } = timeMusic;
}