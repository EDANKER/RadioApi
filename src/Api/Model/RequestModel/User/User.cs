namespace Api.Model.RequestModel.User;

public class User
{
    public User(string fullName,string login, string role)
    {
        FullName = fullName;
        Login = login;
        Role = role;
    }

    public string FullName { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
}