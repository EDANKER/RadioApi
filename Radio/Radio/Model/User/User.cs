namespace Radio.Model.User;

public class User
{
    public User(string tag, string name, string login, bool speak, 
        bool settingsTime, bool settingsUser, bool turnItOnMusic, 
        bool createPlayList, bool saveMusic)
    {
        Tag = tag;
        Name = name;
        Login = login;
        Speak = speak;
        SettingsTime = settingsTime;
        SettingsUser = settingsUser;
        TurnItOnMusic = turnItOnMusic;
        CreatePlayList = createPlayList;
        SaveMusic = saveMusic;
    }

    public string Tag { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public bool Speak { get; set; }
    public bool SettingsTime { get; set; }
    public bool SettingsUser { get; set; }
    public bool TurnItOnMusic { get; set; }
    public bool CreatePlayList { get; set; }
    public bool SaveMusic { get; set; }
}