namespace Radio.Model.RequestModel.User;

public class User
{
    public User(string name,string login, string[] role)
    {
        Name = name;
        Role = role;
    }

    public string Name { get; set; }
    public string[] Role { get; set; }
}