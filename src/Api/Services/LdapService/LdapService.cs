﻿using System.Net;
using Novell.Directory.Ldap;
using Authorization = Api.Model.RequestModel.Authorization.Authorization;

namespace Api.Services.LdapService;

public interface ILdapService
{
    Task<bool> Validation(Authorization authorization);
}

public class LdapService(ILogger<LdapService> logger, IConfiguration configuration) : ILdapService
{
    public async Task<bool> Validation(Authorization authorization)
    {
        LdapConnection ldapConnection = new LdapConnection();
        await ldapConnection.ConnectAsync("10.3.15.204", 389);
        
        try
        {
            await ldapConnection.BindAsync(",cn=users,dc=it-college,dc=ru", authorization.Password);
            return true;
        }
        catch (LdapException e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}