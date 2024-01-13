using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Admin.GetAllUser;

public interface IGetAllUserController
{
    public Task<IActionResult> GetAllUser();
    public Task<IActionResult> GetIdUser(int id);
}

[Route("/api/v1/[controller]")]
[ApiController]
public class GetUserController : ControllerBase, IGetAllUserController
{
    [HttpGet("getAllUser")]
    public async Task<IActionResult> GetAllUser()
    {
        return Ok("Hello");
    }

    [HttpGet("getIdUser{id:int}")]
    public Task<IActionResult> GetIdUser(int id)
    {
        throw new NotImplementedException();
    }
}