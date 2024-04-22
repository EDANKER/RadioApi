using Api.Model.ResponseModel.Scenario;
using Api.Services.ScenarioServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Scenario;

[ApiController]
[Route("api/v1/[controller]")]
public class ScenarioController(IScenarioServices scenarioServices) : ControllerBase
{
    [HttpPost("CreateOrSave")]
    public async Task<IActionResult> CreateOrSave([FromBody] Model.RequestModel.Scenario.Scenario scenario)
    {
        if (await scenarioServices.Search("Scenario", scenario.Name, "Name"))
            return BadRequest("Имя уже занято");

        return Ok(await scenarioServices.CreateOrSave("Scenario", scenario));
    }

    [HttpGet("GetScenarioLimit/{limit:int}")]
    public async Task<IActionResult> GetScenarioLimit(int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение limit");
        List<DtoScenario>? dtoScenario = await scenarioServices.GetLimit("Scenario", limit);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return BadRequest("Таких данных нет");
    }

    [HttpGet("GetScenarioId/{id:int}")]
    public async Task<IActionResult> GetScenarioId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        DtoScenario? dtoScenario = await scenarioServices.GetId("Scenario", id);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return BadRequest("Таких данных нет");
    }

    [HttpDelete("DeleteScenarioId/{id:int}")]
    public async Task<IActionResult> DeleteScenarioId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");

        return Ok(await scenarioServices.DeleteId("Scenario", id));
    }

    [HttpPut("UpdateScenario/{id:int}")]
    public async Task<IActionResult> UpdateScenario([FromBody] Model.RequestModel.Scenario.Scenario scenario, int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");

        return Ok(await scenarioServices.UpdateId("Scenario", scenario, id));
    }
}