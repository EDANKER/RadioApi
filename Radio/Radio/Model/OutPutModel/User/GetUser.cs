namespace Radio.Model.User;

public class GetUser
{
    public GetUser( int id, string tag, string name, Settings.Settings settings)
    {
        Id = id;
        Tag = tag;
        Name = name;
        Settings = settings;
    }

    public int Id { get; set; }
    public string Tag { get; set; }
    public string Name { get; set; }
    public Settings.Settings Settings { get; set; }
}