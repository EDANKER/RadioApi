namespace Radio.Model.Authorization;

public class Authorization
{
    public Authorization(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public string Login { get; set; }
    public string Password { get; set; }
}