using Microsoft.AspNetCore.Mvc;
using Radio.Model.RequestModel.Scenari;

namespace Radio.Controller.SettingsScenari;

public interface ISettingsScenariController
{
    public Task<IActionResult> Time([FromBody]DateTime dateTime);
    public Task<IActionResult> Sector();
    public Task<IActionResult> GetLimitScenario(int limit);
    public Task<IActionResult> DeleteScenario(int id);
    public Task<ApplicationId> UpdateScenario(Scenari scenario, int id);
}

[ApiController]
[Route("api/v1/[controller]")]
public class SettingsScenariController : ControllerBase, ISettingsScenariController
{
    [HttpPost("Time")]
    public Task<IActionResult> Time(DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    [HttpPost("Sector")]
    public Task<IActionResult> Sector()
    { 
        throw new NotImplementedException();
    }

    [HttpGet("GetLimitScenario")]
    public Task<IActionResult> GetLimitScenario(int limit)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("DeleteScenario")]
    public Task<IActionResult> DeleteScenario(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("UpdateScenario")]
    public Task<ApplicationId> UpdateScenario(Scenari scenario, int id)
    {
        throw new NotImplementedException();
    }
}