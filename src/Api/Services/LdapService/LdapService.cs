using Novell.Directory.Ldap;
using Authorization = Api.Model.RequestModel.Authorization.Authorization;

namespace Api.Services.LdapService;

public interface ILdapService
{
    Task<bool> Validation(Authorization authorization);
}

public class LdapService(ILogger<LdapService> logger, 
    IConfiguration configuration
    ) : ILdapService
{
    public async Task<bool> Validation(Authorization authorization)
    {
        LdapConnection ldapConnection = new LdapConnection(new LdapConnectionOptions());
        ldapConnection.SecureSocketLayer = false;
        await ldapConnection.ConnectAsync(configuration.GetSection("Ldap:url").Value, Convert.ToInt32(configuration.GetSection("Ldap:port").Value));
        
        try
        {
            await ldapConnection.BindAsync(2,"uid=" + authorization.Login + configuration.GetSection("Ldap:searchBase").Value, authorization.Password);
            return true;
        }
        catch (LdapException e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}