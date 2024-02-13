using Api.Services.LdapService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radio.Services.GeneratorTokenServices;

namespace Radio.Controller.Authorization.LoginUserController;

public interface ILoginUserController
{
    public Task<IActionResult> Login(Api.Model.Authorization.Authorization authorization);
}
[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController(ILdapService ldapService, IGeneratorTokenServices generatorTokenServices)
    : ControllerBase, ILoginUserController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] Api.Model.Authorization.Authorization authorization)
    {
        if (!await ldapService.Validation(authorization))
            return BadRequest();

        return Ok(generatorTokenServices.Generator(authorization.Login));
    }
}