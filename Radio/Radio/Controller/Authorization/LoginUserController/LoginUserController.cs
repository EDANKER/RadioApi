using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radio.Services.GeneratorTokenServices;
using Radio.Services.LdapConnectService;

namespace Radio.Controller.Authorization.LoginUserController;

public interface ILoginUserController
{
    public Task<IActionResult> Login(Model.Authorization.Authorization authorization);
}
[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController : ControllerBase, ILoginUserController
{
    private ILdapConnectService _ldapConnectService;
    private IGeneratorTokenServices _generatorTokenServices;

    public LoginUserController(ILdapConnectService ldapConnectService, IGeneratorTokenServices generatorTokenServices)
    {
        _ldapConnectService = ldapConnectService;
        _generatorTokenServices = generatorTokenServices;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]Model.Authorization.Authorization authorization)
    {
        if (await _ldapConnectService.Validation(authorization.Login, authorization.Password))
            return BadRequest();

        return Ok(_generatorTokenServices.Generator(authorization.Login));
    }
}