using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.SettingsScenari;

public interface ISettingsScenariController
{
    public Task<IActionResult> Time();
}

[ApiController]
[Route("api/v1/[controller]")]
public class SettingsScenariController : ControllerBase, ISettingsScenariController
{
    [HttpPost("[action]")]
    public Task<IActionResult> Time()
    {
        throw new NotImplementedException();
    }
}