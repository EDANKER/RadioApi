namespace Api.Model.DTOs.User;

public class DtoUser(string fullName, string login, List<string> role)
{
    public string FullName { get; set; } = fullName;
    public string Login { get; set; } = login;
    public List<string> Role { get; set; } = role;
}