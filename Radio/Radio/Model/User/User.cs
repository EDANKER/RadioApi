namespace Radio.Model.User;

public class User
{
    public User(string name, string login, bool speak, bool settingsTime, bool settingsUser, bool turnItOnMusic)
    {
        Name = name;
        Login = login;
        Speak = speak;
        SettingsTime = settingsTime;
        SettingsUser = settingsUser;
        TurnItOnMusic = turnItOnMusic;
    }

    public string Name { get; set; }
    public string Login { get; set; }
    public bool Speak { get; set; }
    public bool SettingsTime { get; set; }
    public bool SettingsUser { get; set; }
    public bool TurnItOnMusic { get; set; }
}