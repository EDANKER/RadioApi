using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.User;

public class User(string fullName, string login, string[] role)
{
    [Required]
    [StringLength(128, MinimumLength = 7, ErrorMessage = "< 128 > 7")]
    public string FullName { get; } = fullName;
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Login { get; } = login;
    [Required]
    public string[] Role { get; } = role;
}