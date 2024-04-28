using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Create.CreatePlayList;

public class CreatePlayList(string name, string description, string imgPath)
{
    [Required]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(203, MinimumLength = 10, ErrorMessage = "< 203 > 10")]
    public string Description { get; } = description;
    [Required]
    [StringLength(203, MinimumLength = 3, ErrorMessage = "< 203 > 3")]
    public string ImgPath { get; } = imgPath;
}