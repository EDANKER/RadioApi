using Api.Services.ScenarioServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Scenario;

public interface IScenarioController
{
    public Task<IActionResult> CreateOrSave(Model.RequestModel.Scenario.Scenario scenario);
    public Task<IActionResult> GetLimitScenario(int limit);
    public Task<IActionResult> GetScenarioId(int id);
    public Task<IActionResult> DeleteScenario(int id);
    public Task<IActionResult> UpdateScenario(Model.RequestModel.Scenario.Scenario scenario, int id);
}

[ApiController]
[Route("api/v1/[controller]")]
public class ScenarioController(IScenarioServices scenarioServices) : ControllerBase, IScenarioController
{

    [HttpPost("SettingsAll")]
    public async Task<IActionResult> CreateOrSave([FromBody]Model.RequestModel.Scenario.Scenario scenario)
    {
        if (await scenarioServices.Search("Scenario", scenario.Name))
            return BadRequest("Имя уже занято");
        
        return Ok(await scenarioServices.CreateOrSave("Scenario", scenario));
    }

    [HttpGet("GetLimitScenario/{limit:int}")]
    public async Task<IActionResult> GetLimitScenario(int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await scenarioServices.GetLimit("Scenario", limit));
    }

    [HttpGet("GetScenarioId/{id:int}")]
    public async Task<IActionResult> GetScenarioId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await scenarioServices.GetId("Scenario", id));
    }

    [HttpDelete("DeleteScenario/{id:int}")]
    public async Task<IActionResult> DeleteScenario(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await scenarioServices.DeleteId("Scenario", id));
    }

    [HttpPut("UpdateScenario/{id:int}")]
    public async Task<IActionResult> UpdateScenario([FromBody]Model.RequestModel.Scenario.Scenario scenario, int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await scenarioServices.Update("Scenario", scenario, id));
    }
}