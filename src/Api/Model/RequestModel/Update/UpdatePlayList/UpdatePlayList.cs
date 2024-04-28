using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Update.UpdatePlayList;

public class UpdatePlayList(string name, string description)
{
    [Required]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(1000, MinimumLength = 3, ErrorMessage = "< 1000 > 3")]
    public string Description { get; } = description;
}