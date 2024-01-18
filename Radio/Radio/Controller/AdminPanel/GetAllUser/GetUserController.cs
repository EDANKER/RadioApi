using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Admin.GetAllUser;

public interface IGetUserController
{
    public Task<IActionResult> GetLimitUser(int limit);
    public Task<IActionResult> GetNameUser(string name);
}

[Route("/api/v1/[controller]")]
[ApiController]
public class GetUserController : ControllerBase, IGetUserController
{
    [HttpGet("[action]{limit:int}")]
    [EnableCors("RadioWeb")]
    public async Task<IActionResult> GetLimitUser(int limit)
    {
        return Ok("Hello");
    }

    [HttpGet("[action]{name}")]
    public Task<IActionResult> GetNameUser(string name)
    {
        throw new NotImplementedException();
    }
}