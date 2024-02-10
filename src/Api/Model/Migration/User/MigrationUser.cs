namespace Api.Model.Migration.User;

public class MigrationUser(int id, string fullName, string login)
{
    public int Id { get; set; } = id;
    public string FullName { get; set; } = fullName;
    public string Login { get; set; } = login;
}