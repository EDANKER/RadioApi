using Api.Model.RequestModel.Scenario;
using Api.Services.ScenarioServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.SettingsScenari;

public interface ISettingsScenarioController
{
    public Task<IActionResult> SettingsAll(Scenario scenario);
    public Task<IActionResult> GetLimitScenario(int limit);
    public Task<IActionResult> GetScenarioId(int id);
    public Task<IActionResult> DeleteScenario(int id);
    public Task<IActionResult> UpdateScenario(Scenario scenario, int id);
}

[ApiController]
[Route("api/v1/[controller]")]
public class SettingsScenarioController(IScenarioServices scenarioServices) : ControllerBase, ISettingsScenarioController
{

    [HttpPost("SettingsAll")]
    public async Task<IActionResult> SettingsAll([FromBody]Scenario scenario)
    {
        if (await scenarioServices.Search("Scenario", scenario.Name))
            return BadRequest();
        
        return Ok(await scenarioServices.CreateOrSave("Scenario", scenario));
    }

    [HttpGet("GetLimitScenario/{limit:int}")]
    public async Task<IActionResult> GetLimitScenario(int limit)
    {
        return Ok(await scenarioServices.GetLimit("Scenario", limit));
    }

    [HttpGet("GetScenarioId/{id:int}")]
    public async Task<IActionResult> GetScenarioId(int id)
    {
        return Ok(await scenarioServices.GetId("Scenario", id));
    }

    [HttpDelete("DeleteScenario/{id:int}")]
    public async Task<IActionResult> DeleteScenario(int id)
    {
        return Ok(await scenarioServices.DeleteId("Scenario", id));
    }

    [HttpPut("UpdateScenario/{id:int}")]
    public async Task<IActionResult> UpdateScenario([FromBody]Scenario scenario, int id)
    {
        return Ok(await scenarioServices.Update("Scenario", scenario, id));
    }
}