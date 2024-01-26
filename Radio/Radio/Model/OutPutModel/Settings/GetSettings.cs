namespace Radio.Model.Settings;

public class GetSettings
{
    public GetSettings(bool speak, bool settingsTime, bool settingsUser, bool turnItOnMusic, bool createPlayList, bool saveMusic, bool settingsScinaria, bool turnOnSector)
    {
        Speak = speak;
        SettingsTime = settingsTime;
        SettingsUser = settingsUser;
        TurnItOnMusic = turnItOnMusic;
        CreatePlayList = createPlayList;
        SaveMusic = saveMusic;
        SettingsScinaria = settingsScinaria;
        TurnOnSector = turnOnSector;
    }

    public bool Speak { get; set; }
    public bool SettingsTime { get; set; }
    public bool SettingsUser { get; set; }
    public bool TurnItOnMusic { get; set; }
    public bool CreatePlayList { get; set; }
    public bool SaveMusic { get; set; }
    public bool SettingsScinaria { get; set; }
    public bool TurnOnSector { get; set; }
}