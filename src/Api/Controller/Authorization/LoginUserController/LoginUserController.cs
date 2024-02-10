using Api.Services.LdapConnectService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radio.Services.GeneratorTokenServices;

namespace Radio.Controller.Authorization.LoginUserController;

public interface ILoginUserController
{
    public Task<IActionResult> Login(Model.Authorization.Authorization authorization);
}
[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController(ILdapConnectService ldapConnectService, IGeneratorTokenServices generatorTokenServices)
    : ControllerBase, ILoginUserController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]Model.Authorization.Authorization authorization)
    {
        if (!await ldapConnectService.Validation(authorization.Login, authorization.Password))
            return BadRequest();

        return Ok(generatorTokenServices.Generator(authorization.Login));
    }
}