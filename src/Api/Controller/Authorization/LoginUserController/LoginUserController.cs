using Api.Services.LoginUserServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Authorization.LoginUserController;

[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController(ILoginUserServices loginUserServices)
    : ControllerBase
{
    [HttpPost("Login")]
    [Consumes("application/json")]
    public async Task<IActionResult> Login([FromBody] Api.Model.RequestModel.Authorization.Authorization authorization)
    {
        string? authenticationUser = await loginUserServices.AuthenticationUser(authorization);
        if (authenticationUser != null)
            return Ok(authenticationUser);

        return Content("status 204");
    }
}