using Api.Services.GeneratorTokenServices;
using Api.Services.LdapService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Authorization.LoginUserController;

[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController(ILdapService ldapService, IGeneratorTokenServices generatorTokenServices)
    : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] Api.Model.RequestModel.Authorization.Authorization authorization)
    {
        if (!await ldapService.Validation(authorization))
            return BadRequest("не верные данные");
        
        return Ok(await generatorTokenServices.Generator(authorization.Login));
    }
}