using Api.Services.ScenarioServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Scenario;

[ApiController]
[Route("api/v1/[controller]")]
public class ScenarioController(IScenarioServices scenarioServices) : ControllerBase
{

    [HttpPost("CreateOrSave")]
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