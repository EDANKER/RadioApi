
namespace Radio.Model.ResponseModel.User;

public class GetUser
{
    public GetUser(int id, string name, string[] role)
    {
        Id = id;
        Role = role;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string[] Role { get; set; }
}