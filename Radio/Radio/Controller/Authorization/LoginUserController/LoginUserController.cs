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
    private ILdapConnectService _connectService;
    private IGeneratorTokenServices _generatorTokenServices;

    public LoginUserController(ILdapConnectService connectService, IGeneratorTokenServices generatorTokenServices)
    {
        _connectService = connectService;
        _generatorTokenServices = generatorTokenServices;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]Model.Authorization.Authorization authorization)
    {
        if (!await _connectService.Validation(authorization.Login, authorization.Password))
            return BadRequest();

        return Ok(_generatorTokenServices.Generator(authorization.Login));
    }
}