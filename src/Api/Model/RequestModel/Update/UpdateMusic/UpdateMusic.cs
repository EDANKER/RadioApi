using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Update.UpdateMusic;

public class UpdateMusic(string name, string namePlayList)
{
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string NamePlayList { get; } = namePlayList;
}