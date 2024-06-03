using System.ComponentModel.DataAnnotations;
using Api.Model.ResponseModel.Scenario;
using Api.Services.ScenarioServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Scenario;

[ApiController]
[Route("api/v1/[controller]")]
public class ScenarioController(IScenarioServices scenarioServices) : ControllerBase
{
    [HttpPost("CreateOrSave")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateOrSave([FromBody] Model.RequestModel.Scenario.Scenario scenario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await scenarioServices.Search("Scenario", scenario.Name, "Name"))
            return BadRequest("Имя уже занято");
        double? validationTime = await scenarioServices.ValidationTime(scenario.Time);
        if (validationTime != null)
            return BadRequest(validationTime);

        return Ok(await scenarioServices.CreateOrSave("Scenario", scenario));
    }

    [HttpGet("GetScenarioLimit")]
    public async Task<IActionResult> GetScenarioLimit([Required] [FromQuery] int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение limit");
        List<DtoScenario>? dtoScenario = await scenarioServices.GetLimit("Scenario", limit);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return Content("status 204");
    }

    [HttpGet("GetScenarioId")]
    public async Task<IActionResult> GetScenarioId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        DtoScenario? dtoScenario = await scenarioServices.GetId("Scenario", id);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return Content("status 204");
    }

    [HttpDelete("DeleteScenarioId")]
    public async Task<IActionResult> DeleteScenarioId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");

        return Ok(await scenarioServices.DeleteId("Scenario", id));
    }

    [HttpPut("UpdateScenario")]
    public async Task<IActionResult> UpdateScenario([Required] [FromBody] Model.RequestModel.Scenario.Scenario scenario,
        [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");

        return Ok(await scenarioServices.UpdateId("Scenario", scenario, id));
    }
}