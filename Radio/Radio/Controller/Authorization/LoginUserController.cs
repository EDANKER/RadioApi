using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Authorization;

public interface ILoginUserController
{
    public Task<IActionResult> Login(string login, string password);
}
[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController : ControllerBase, ILoginUserController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login(string login, string password)
    {
        return Ok();
    }
}