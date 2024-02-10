using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.TurnOnSector;

public interface ISettingsSectorController
{
    public Task<IActionResult> BroadcastOn(int id);
}

[ApiController]
[Route("api/v1/[controller]")]
public class SettingsSectorController : ControllerBase, ISettingsSectorController
{
    [HttpPost("BroadcastOn{id:int}")]
    public Task<IActionResult> BroadcastOn(int id)
    {
        throw new NotImplementedException();
    }
}