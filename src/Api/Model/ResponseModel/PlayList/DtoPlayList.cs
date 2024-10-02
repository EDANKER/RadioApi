using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.PlayList;

public class DtoPlayList(int id, string name, string description, string imgPath)
{
    [Required] public int Id { get; set; } = id;

    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Name { get; set; } = name;

    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Description { get; set; } = description;

    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string ImgPath { get; set; } = imgPath;
}