namespace Api.Model.Authorization;

public class Authorization(string login, string password)
{
    public string Login { get; set; } = login;
    public string Password { get; set; } = password;
}