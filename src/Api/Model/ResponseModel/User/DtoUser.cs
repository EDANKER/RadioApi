using System.ComponentModel.DataAnnotations;

namespace Api.Model.ResponseModel.User;

public class DtoUser(int id, string fullName, string login, string role)
{
    public int Id { get; set; } = id;
    [Required]
    [StringLength(120, MinimumLength = 7, ErrorMessage = "< 120 > 7")]
    public string FullName { get; set; } = fullName;
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Login { get; set; } = login;
    public string Role { get; set; } = role;
}