using System.ComponentModel.DataAnnotations;
using Api.Model.ResponseModel.PlayScenario;
using Api.Services.ScenarioServices.ScnearioServicesTime;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Scenario.ScenarioPlayController;

[ApiController]
[Route("api/v1/[controller]")]
public class ScenarioPlayController(IScenarioServicesTime scenarioServices) : ControllerBase
{
    [HttpPost("CreateOrSave")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateOrSave([FromBody] Model.RequestModel.Scenario.Scenario scenario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (await scenarioServices.Search("Scenario", scenario.Name, "Name"))
            return BadRequest("Имя уже занято");
        if (!await scenarioServices.ValidationTime(scenario.Time, scenario.Days))
            return BadRequest("Такое время уже занято");
        
        DtoPlayScenario? dtoScenario = await scenarioServices.CreateOrSave("Scenario", scenario);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return BadRequest();
    }

    [HttpGet("GetScenarioLimit")]
    public async Task<IActionResult> GetScenarioLimit([Required] [FromQuery] int limit,
        [Required] [FromQuery] int currentPage)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение limit");
        List<DtoPlayScenario>? dtoScenario = await scenarioServices.GetLimit("Scenario", currentPage, limit);
        if (dtoScenario != null)
        {
            var response = new
            {
                Head = await scenarioServices.GetCountPage("Scenario", currentPage, limit),
                Body = dtoScenario
            };

            return Ok(response);
        }

        return Content("status 204");
    }

    [HttpGet("GetScenarioId")]
    public async Task<IActionResult> GetScenarioId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        DtoPlayScenario? dtoScenario = await scenarioServices.GetId("Scenario", id);
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
        
        DtoPlayScenario? dtoScenario = await scenarioServices.UpdateId("Scenario", scenario, id);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return BadRequest();
    }
}