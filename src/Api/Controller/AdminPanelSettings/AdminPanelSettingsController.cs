using System.ComponentModel.DataAnnotations;
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
    [Consumes("application/json")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (await userServices.Search("Users", user.FullName, "FullName"))
            return BadRequest("Такие данные уже есть");

        return Ok(await userServices.CreateOrSave("Users", user));
    }

    [HttpDelete("DeleteUserId")]
    public async Task<IActionResult> DeleteUserId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await userServices.DeleteId("Users", id));
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] User user, [Required] [FromQuery] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await userServices.UpdateId("Users", user, id));
    }

    [HttpGet("GetUserLimit")]
    public async Task<IActionResult> GetUserLimit([Required] [FromQuery] int limit, [Required] [FromQuery] int currentPage)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение id");
        
        List<DtoUser>? dtoScenario = await userServices.GetLimit("Users", currentPage, limit);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return Content("status 204");
    }

    [HttpGet("GetUserId")]
    public async Task<IActionResult> GetUserId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        DtoUser? dtoScenario = await userServices.GetId("Users", id);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return Content("status 204");
    }
}