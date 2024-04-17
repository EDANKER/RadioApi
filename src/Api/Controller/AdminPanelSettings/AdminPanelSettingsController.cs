using Api.Model.RequestModel.User;
using Api.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.AdminPanelSettings;

[Route("api/v1/[controller]")]
[ApiController]
public class AdminPanelSettingsController(IUserServices userServices) : ControllerBase
{
    [HttpPost("CreateNewUser")]
    public async Task<IActionResult> CreateNewUser([FromBody] User user)
    {
        if (await userServices.Search("Users", user.FullName, user.Login))
            return BadRequest("Такие данные уже есть");
        
        return Ok(await userServices.CreateOrSave("Users", user));
    }

    [HttpDelete("DeleteUserId/{id:int}")]
    public async Task<IActionResult> DeleteUserId(int id)
    {
        return Ok(await userServices.DeleteId("Users", id));
    }

    [HttpPut("UpdateUser/{id:int}")]
    public async Task<IActionResult> UpdateUser([FromBody] User user, int id)
    {
        return Ok(await userServices.Update("Users", user, id));
    }

    [HttpGet("GetLimitUser/{limit:int}")]
    public async Task<IActionResult> GetUser(int limit)
    {
        return Ok(await userServices.GetLimitUser("Users", limit));
    }

    [HttpGet("GetIdUser/{id:int}")]
    public async Task<IActionResult> GetIdUser(int id)
    {
        return Ok(await userServices.GetIdUser("Users", id));
    }
}