using Microsoft.AspNetCore.Mvc;
using Radio.Data.LdapConnect;
using Radio.Services.GeneratorTokenServices;

namespace Radio.Controller.Authorization.LoginUserController;

public interface ILoginUserController
{
    public Task<IActionResult> Login(Model.Authorization.Authorization authorization);
}
[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController : ControllerBase, ILoginUserController
{
    private ILdapConnect _connect;
    private IGeneratorTokenServices _generatorTokenServices;

    public LoginUserController(ILdapConnect connect, IGeneratorTokenServices generatorTokenServices)
    {
        _connect = connect;
        _generatorTokenServices = generatorTokenServices;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]Model.Authorization.Authorization authorization)
    {
        // if (!await _connect.Validation(authorization.Login, authorization.Password))
        //     return NoContent();

        return Ok(_generatorTokenServices.Generator(authorization.Login, authorization.Password));
    }
}