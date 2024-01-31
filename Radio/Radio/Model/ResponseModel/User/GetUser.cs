namespace Radio.Model.User;

public class GetUser
{
    public GetUser( int id, string tag, string name, bool speak, bool settingsTime, bool settingsUser, bool turnItOnMusic, bool createPlayList, bool saveMusic, bool settingsScinaria, bool turnOnSector)
    {
        Id = id;
        Tag = tag;
        Name = name;
        Speak = speak;
        SettingsTime = settingsTime;
        SettingsUser = settingsUser;
        TurnItOnMusic = turnItOnMusic;
        CreatePlayList = createPlayList;
        SaveMusic = saveMusic;
        SettingsScinaria = settingsScinaria;
        TurnOnSector = turnOnSector;
    }

    public int Id { get; set; }
    public string Tag { get; set; }
    public string Name { get; set; }
    public bool Speak { get; set; }
    public bool SettingsTime { get; set; }
    public bool SettingsUser { get; set; }
    public bool TurnItOnMusic { get; set; }
    public bool CreatePlayList { get; set; }
    public bool SaveMusic { get; set; }
    public bool SettingsScinaria { get; set; }
    public bool TurnOnSector { get; set; }
}