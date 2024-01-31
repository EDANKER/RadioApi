namespace Radio.Model.Settings;

public class Settings
{
    public Settings(bool speak, bool settingsUser, bool turnOnMusic, bool createPlayList, bool settingsMusic, bool settingsScinaria, bool turnOnSector)
    {
        Speak = speak;
        SettingsUser = settingsUser;
        TurnOnMusic = turnOnMusic;
        CreatePlayList = createPlayList;
        SettingsMusic = settingsMusic;
        SettingsScinaria = settingsScinaria;
        TurnOnSector = turnOnSector;
    }

    public bool Speak { get; set; }
    public bool SettingsUser { get; set; }
    public bool TurnOnMusic { get; set; }
    public bool CreatePlayList { get; set; }
    public bool SettingsMusic { get; set; }
    public bool SettingsScinaria { get; set; }
    public bool TurnOnSector { get; set; }
}