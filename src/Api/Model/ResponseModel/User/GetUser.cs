
namespace Radio.Model.ResponseModel.User;

public class GetUser
{
    public GetUser(int id, string fullName, string login, string role)
    {
        Id = id;
        FullName = fullName;
        Login = login;
        Role = role;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
}