using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.TurnOnSector;

public interface ISettingsSectorController
{
    public Task<IActionResult> GetData();
    public Task<IActionResult> BroadcastOn();
}

[ApiController]
[Route("api/v1/[controller]")]
public class SettingsSectorController : ControllerBase, ISettingsSectorController
{
    [HttpGet("GetData")]
    public async Task<IActionResult> GetData()
    {
        return Ok();
    }

    [HttpPost("BroadcastOn{id:int}")]
    public Task<IActionResult> BroadcastOn()
    {
        throw new NotImplementedException();
    }
}