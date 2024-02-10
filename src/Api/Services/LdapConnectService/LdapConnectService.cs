using System.DirectoryServices.Protocols;
using System.Net;

namespace Api.Services.LdapConnectService;

public interface ILdapConnectService
{
    public Task<bool> Validation(string id, string password);
}

public class LdapConnectService(ILogger<LdapConnectService> logger, IConfiguration configuration) : ILdapConnectService
{
    public Task<bool> Validation(string id, string password)
    {
        LdapDirectoryIdentifier directoryIdentifier =
            new LdapDirectoryIdentifier(configuration.GetSection("Ldap:url").Value, false, false);
        NetworkCredential networkCredential =
            new NetworkCredential("uid=" + id + configuration.GetSection("Ldap:searchBase").Value, password);

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
            logger.LogError(e.ToString());
            return Task.FromResult(false);
        }
    }
}