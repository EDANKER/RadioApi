using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestModel.Authorization;

public class Authorization(string login, string password)
{   
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "< 64 > 3")]
    public string Login { get; } = login;
    [Required]
    [StringLength(203, MinimumLength = 7, ErrorMessage = "< 203 > 7")]
    public string Password { get; } = password;
}