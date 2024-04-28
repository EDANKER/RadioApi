using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Update.UpdateMusic;

public class UpdateMusic(string name, string namePlayList)
{
    [Required]
    [StringLength(203, MinimumLength = 3, ErrorMessage = "< 203 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string NamePlayList { get; } = namePlayList;
}