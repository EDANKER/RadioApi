namespace Radio.Model.User;

public class GetUser
{
    public GetUser(int id, string[] role, string name, bool speak, bool settingsTime, bool settingsUser, bool turnOnMusic,
        bool createPlayList, bool saveMusic, bool settingsScinaria, bool turnOnSector)
    {
        Id = id;
        Role = role;
        Name = name;
        Speak = speak;
        SettingsTime = settingsTime;
        SettingsUser = settingsUser;
        TurnOnMusic = turnOnMusic;
        CreatePlayList = createPlayList;
        SaveMusic = saveMusic;
        SettingsScinaria = settingsScinaria;
        TurnOnSector = turnOnSector;
    }

    public int Id { get; set; }
    public string[] Role { get; set; }
    public string Name { get; set; }
    public bool Speak { get; set; }
    public bool SettingsTime { get; set; }
    public bool SettingsUser { get; set; }
    public bool TurnOnMusic { get; set; }
    public bool CreatePlayList { get; set; }
    public bool SaveMusic { get; set; }
    public bool SettingsScinaria { get; set; }
    public bool TurnOnSector { get; set; }
}