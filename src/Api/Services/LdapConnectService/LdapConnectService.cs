using System.DirectoryServices.Protocols;
using System.Net;

namespace Api.Services.LdapConnectService;

public interface ILdapConnectService
{
    public Task<bool> Validation(string id, string password);
}

public class LdapConnectServiceService(IConfiguration configuration) : ILdapConnectService
{
    private IConfiguration _configuration = configuration;

    public Task<bool> Validation(string id, string password)
    {
        LdapDirectoryIdentifier directoryIdentifier = new LdapDirectoryIdentifier(_configuration.GetSection("Ldap:url").Value, false, false);
        NetworkCredential networkCredential =
            new NetworkCredential("uid=" + id + _configuration.GetSection("Ldap:searchBase").Value, password);

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