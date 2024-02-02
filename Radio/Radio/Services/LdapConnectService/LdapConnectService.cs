using System.DirectoryServices.Protocols;
using System.Net;
using LdapConnection = System.DirectoryServices.Protocols.LdapConnection;

namespace Radio.Data.LdapConnect;

public interface ILdapConnect
{
    public Task<bool> Validation(string id, string password);
}

public class LdapConnectService : ILdapConnect
{
    public Task<bool> Validation(string id, string password)
    {
        try
        {
            LdapDirectoryIdentifier directoryIdentifier = new LdapDirectoryIdentifier("ldap.it-college.ru:389", false, false);
            NetworkCredential networkCredential = new NetworkCredential("uid=" + id + ",ou=people,ou=Students,dc=it-college,dc=ru", password);

            LdapConnection ldapConnection = new LdapConnection(directoryIdentifier, networkCredential, AuthType.Basic);
            ldapConnection.SessionOptions.SecureSocketLayer = false;
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