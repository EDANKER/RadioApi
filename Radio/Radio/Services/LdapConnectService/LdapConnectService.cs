using System.DirectoryServices.Protocols;
using System.Net;
using LdapConnection = System.DirectoryServices.Protocols.LdapConnection;

namespace Radio.Services.LdapConnectService;

public interface ILdapConnectService
{
    public Task<bool> Validation(string id, string password);
}

public class LdapConnectServiceService : ILdapConnectService
{
    public Task<bool> Validation(string id, string password)
    {
        try
        {
            LdapDirectoryIdentifier directoryIdentifier = new LdapDirectoryIdentifier("10.3.0.9:389", false, false);
            NetworkCredential networkCredential = new NetworkCredential("uid=" + id + ",ou=people,ou=Students,dc=it-college,dc=ru", password);

            LdapConnection ldapConnection = new LdapConnection(directoryIdentifier, networkCredential, AuthType.Basic);
            ldapConnection.SessionOptions.SecureSocketLayer = true;
            ldapConnection.SessionOptions.ProtocolVersion = 3;

            ldapConnection.Bind();
        
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(false);
        }
    }
}