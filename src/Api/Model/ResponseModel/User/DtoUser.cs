using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.User;

public class DtoUser(int id, string fullName, string login, string role)
{
    [Required] public int Id { get; set; } = id;

    [Required]
    [StringLength(255, MinimumLength = 7, ErrorMessage = "< 255 > 7")]
    public string FullName { get; set; } = fullName;

    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Login { get; set; } = login;

    [Required]
    [StringLength(255, MinimumLength = 7, ErrorMessage = "< 255 > 7")]
    public string Role { get; set; } = role;
}