using Api.Model.RequestModel.Authorization;
using Api.Services.GeneratorTokenServices;
using Api.Services.LdapService;
using Api.Services.UserServices;

namespace Api.Services.LoginUserServices;

public interface ILoginUserServices
{
    Task<string?> AuthenticationUser(Authorization authorization);
}

public class LoginUserServices(
    ILdapService ldapService,
    IGeneratorTokenServices generatorTokenServices,
    IUserServices userServices) : ILoginUserServices
{
    public async Task<string?> AuthenticationUser(Authorization authorization)
    {
        if (await ldapService.Validation(authorization) 
            && await userServices.Search("Users", authorization.Login, "Login"))
            return await generatorTokenServices.Generator(authorization.Login);

        return null;
    }
}