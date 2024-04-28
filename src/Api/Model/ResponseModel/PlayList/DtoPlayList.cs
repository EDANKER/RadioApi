using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.PlayList;

public class DtoPlayList(int id, string name, string description, string imgPath)
{
    public int Id { get; set; } = id;
    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string ImgPath { get; set; } = imgPath;
}