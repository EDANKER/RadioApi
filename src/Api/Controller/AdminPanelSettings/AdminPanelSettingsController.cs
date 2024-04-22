using Api.Model.RequestModel.User;
using Api.Model.ResponseModel.User;
using Api.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.AdminPanelSettings;

[Route("api/v1/[controller]")]
[ApiController]
public class AdminPanelSettingsController(IUserServices userServices) : ControllerBase
{
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (await userServices.Search("Users", user.FullName, "FullName"))
            return BadRequest("Такие данные уже есть");

        return Ok(await userServices.CreateOrSave("Users", user));
    }

    [HttpDelete("DeleteUserId/{id:int}")]
    public async Task<IActionResult> DeleteUserId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await userServices.DeleteId("Users", id));
    }

    [HttpPut("UpdateUser/{id:int}")]
    public async Task<IActionResult> UpdateUser([FromBody] User user, int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await userServices.UpdateId("Users", user, id));
    }

    [HttpGet("GetUserLimit/{limit:int}")]
    public async Task<IActionResult> GetUserLimit(int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение id");
        
        List<DtoUser>? dtoScenario = await userServices.GetLimit("Users", limit);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return BadRequest("Таких данных нет");
    }

    [HttpGet("GetUserId/{id:int}")]
    public async Task<IActionResult> GetUserId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        DtoUser? dtoScenario = await userServices.GetId("Users", id);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return BadRequest("Таких данных нет");
    }
}