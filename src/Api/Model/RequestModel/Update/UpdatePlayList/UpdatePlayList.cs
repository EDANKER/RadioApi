namespace Api.Model.RequestModel.Update.UpdatePlayList;

public class UpdatePlayList(string name, string description)
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}