using System.Net;
using Microsoft.AspNetCore.Mvc;
using Novell.Directory.Ldap;
using Radio.Data.LdapConnect;

namespace Radio.Controller.Authorization.LoginUserController;

public interface ILoginUserController
{
    public Task<IActionResult> Login(string login, string password);
}
[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController : ControllerBase, ILoginUserController
{
    private ILdapConnect _connect;

    public LoginUserController(ILdapConnect connect)
    {
        _connect = connect;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(string login, string password)
    {
        if (!await _connect.Validation(login, password))
            return NoContent();

        return Ok();
    }
}