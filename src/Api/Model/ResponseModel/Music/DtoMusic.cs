using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.Music;

public class DtoMusic(int id, string name, string namePLayList, string timeMusic)
{
    public int Id { get; set; } = id;
    [StringLength(203, MinimumLength = 3, ErrorMessage = "< 203 > 3")]
    public string Name { get; set; } = name;
    [StringLength(128, MinimumLength = 3, ErrorMessage = "< 128 > 3")]
    public string NamePLayList { get; set; } = namePLayList;
    [StringLength(32, MinimumLength = 3, ErrorMessage = "< 32 > 3")]
    public string TimeMusic { get; set; } = timeMusic;
}