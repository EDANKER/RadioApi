using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Authorization;

public class Authorization(
    string login, 
    string password)
{
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "< 255 > 3")]
    public string Login { get; } = login;

    [Required]
    [StringLength(255, MinimumLength = 7, ErrorMessage = "< 255 > 7")]
    public string Password { get; } = password;
}