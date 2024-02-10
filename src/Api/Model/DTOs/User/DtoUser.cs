namespace Api.DTO.User;

public class DtoUser(int id, string fullName, string login, string role)
{
    public int Id { get; set; } = id;
    public string FullName { get; set; } = fullName;
    public string Login { get; set; } = login;
    public string Role { get; set; } = role;
}