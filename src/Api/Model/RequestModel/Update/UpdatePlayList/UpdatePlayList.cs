using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Update.UpdatePlayList;

public class UpdatePlayList(string name, string description)
{
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; } = name;
    [Required]
    public string Description { get; } = description;
}