namespace Radio.Model;

public class User
{
    public User(string name, string login, bool speak, bool settingsTime, bool settingsUser)
    {
        Name = name;
        Login = login;
        Speak = speak;
        SettingsTime = settingsTime;
        SettingsUser = settingsUser;
    }

    public string Name { get; set; }
    public string Login { get; set; }
    public bool Speak { get; set; }
    public bool SettingsTime { get; set; }
    public bool SettingsUser { get; set; }
}