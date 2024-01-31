namespace Radio.Model.User;

public class User
{
    public User(string tag, string name,string login, Settings.Settings settings)
    {
        Tag = tag;
        Name = name;
        Login = login;
        Settings = settings;
    }

    public string Tag { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public Settings.Settings Settings { get; set; }
}