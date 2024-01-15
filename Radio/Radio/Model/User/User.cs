namespace Radio.Model;

public class User
{
    public User(string name, string login, bool speak, bool settingsTime, bool settingsUser, bool turnTtOnMusic)
    {
        Name = name;
        Login = login;
        Speak = speak;
        SettingsTime = settingsTime;
        SettingsUser = settingsUser;
        TurnTtOnMusic = turnTtOnMusic;
    }

    public string Name { get; set; }
    public string Login { get; set; }
    public bool Speak { get; set; }
    public bool SettingsTime { get; set; }
    public bool SettingsUser { get; set; }
    public bool TurnTtOnMusic { get; set; }
}