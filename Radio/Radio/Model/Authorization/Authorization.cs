namespace Radio.Model.Authorization;

public class Authorization
{
    public Authorization(int id,string login, string password)
    {
        Id = id;
        Login = login;
        Password = password;
    }

    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}