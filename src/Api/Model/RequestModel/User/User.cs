using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.User;

public class User(string fullName, string login, string[] role)
{
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 7")]
    public string FullName { get; } = fullName;
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Login { get; } = login;
    [Required]
    public string[] Role { get; } = role;
}