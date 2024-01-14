using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Authorization;

public interface ILoginUserController
{
    public Task<IActionResult> LoginUser(string login, string password);
}
[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController : ControllerBase, ILoginUserController
{
    [HttpPost("loginUser")]
    public Task<IActionResult> LoginUser(string login, string password)
    {
        throw new NotImplementedException();
    }
}