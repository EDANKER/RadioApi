using System.DirectoryServices.Protocols;
using System.Net;
using Authorization = Api.Model.Authorization.Authorization;

namespace Api.Services.LdapService;

public interface ILdapService
{
    public Task<bool> Validation(Authorization authorization);
}

public class LdapService(ILogger<LdapService> logger, IConfiguration configuration) : ILdapService
{
    public Task<bool> Validation(Authorization authorization)
    {
        LdapDirectoryIdentifier directoryIdentifier =
            new LdapDirectoryIdentifier(configuration.GetSection("Ldap:url").Value, false, false);
        NetworkCredential networkCredential =
            new NetworkCredential("uid=" + authorization.Login + configuration.GetSection("Ldap:searchBase").Value, authorization.Password);

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