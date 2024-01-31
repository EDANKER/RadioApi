using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.TurnOnSector;

public interface ISettingsSectorController
{
    public Task<IActionResult> GetData();
    public Task<IActionResult> Broadcast();
}

public class SettingsSectorController : ControllerBase, ISettingsSectorController
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetData()
    {
        return Ok();
    }

    public Task<IActionResult> Broadcast()
    {
        throw new NotImplementedException();
    }
}