using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Create.CreatePlayList;

public class CreatePlayList(string name, string description, string imgPath)
{
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; } = name;
    [Required]
    [StringLength(255, MinimumLength = 10, ErrorMessage = "< 255 > 10")]
    public string Description { get; } = description;
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string ImgPath { get; } = imgPath;
}