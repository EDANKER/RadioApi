using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.SettingsScenari;

public interface ISettingsScenariController
{
    public Task<IActionResult> Time();
}

public class SettingsScenariController : ControllerBase, ISettingsScenariController
{
    public Task<IActionResult> Time()
    {
        throw new NotImplementedException();
    }
}