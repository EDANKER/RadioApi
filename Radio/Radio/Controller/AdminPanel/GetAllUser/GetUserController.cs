using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Admin.GetAllUser;

public interface IGetAllUserController
{
    public Task<IActionResult> GetAllUser();
    public Task<IActionResult> GetIdUser(int id);
}

[Route("/api/v1/[controller]")]
[ApiController]
public class GetUserController : IGetAllUserController
{
    [HttpGet("getAllUser")]
    public Task<IActionResult> GetAllUser()
    {
        throw new NotImplementedException();
    }

    [HttpGet("getIdUser{id:int}")]
    public Task<IActionResult> GetIdUser(int id)
    {
        throw new NotImplementedException();
    }
}