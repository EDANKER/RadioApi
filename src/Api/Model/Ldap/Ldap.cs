namespace Api.Model.Ldap;

public class Ldap(string address, string path)
{
    public string Address { get; set; } = address;
    public string Path { get; set; } = path;
}