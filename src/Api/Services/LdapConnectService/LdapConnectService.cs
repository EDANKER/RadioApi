using System.DirectoryServices.Protocols;
using System.Net;
namespace Radio.Services.LdapConnectService;

public interface ILdapConnectService
{
    public Task<bool> Validation(string id, string password);
}

public class LdapConnectServiceService : ILdapConnectService
{
    public Task<bool> Validation(string id, string password)
    {
        LdapDirectoryIdentifier directoryIdentifier = new LdapDirectoryIdentifier("10.3.0.9:389", false, false);
        NetworkCredential networkCredential =
            new NetworkCredential("uid=" + id + ",ou=people,ou=Students,dc=it-college,dc=ru", password);

        LdapConnection ldapConnection = new LdapConnection(directoryIdentifier, networkCredential, AuthType.Basic);
        ldapConnection.SessionOptions.SecureSocketLayer = false;
        ldapConnection.SessionOptions.ProtocolVersion = 3;
        
        try
        {
            ldapConnection.Bind();
            return Task.FromResult(true);
        }
        catch (LdapException e)
        {
            Console.WriteLine(e);
            return Task.FromResult(false);
        }
    }
}