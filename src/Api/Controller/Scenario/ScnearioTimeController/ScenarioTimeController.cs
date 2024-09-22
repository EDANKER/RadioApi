using System.ComponentModel.DataAnnotations;
using Api.Model.RequestModel.Scenario.TimeScenario;
using Api.Model.ResponseModel.TimeScenario;
using Api.Services.ScenarioServices.ScnearioServicesTime;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Scenario.ScnearioTimeController;

[ApiController]
[Route("api/v1/[controller]")]
public class ScenarioTimeController(IScenarioTimeServices scenarioServices) : ControllerBase
{
    [HttpPost("CreateOrSave")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateOrSave([FromBody] TimeScenario scenario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (await scenarioServices.Search("TimeMigrationsScenarios", scenario.Name, "Name"))
            return BadRequest("Имя уже занято");
        if(!await scenarioServices.ValidationTime(scenario.Time, scenario.Days))
            return BadRequest("Время уже занято");
        
        DtoTimeScenario? dtoScenario = await scenarioServices.CreateOrSave("TimeScenarios", scenario);
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
        List<DtoTimeScenario>? dtoScenario = await scenarioServices.GetLimit("TimeScenarios", currentPage, limit);
        if (dtoScenario != null)
        {
            var response = new
            {
                Head = await scenarioServices.GetCountPage("TimeScenarios", currentPage, limit),
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
        DtoTimeScenario? dtoScenario = await scenarioServices.GetId("TimeScenarios", id);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return Content("status 204");
    }

    [HttpDelete("DeleteScenarioId")]
    public async Task<IActionResult> DeleteScenarioId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");

        return Ok(await scenarioServices.DeleteId("TimeScenarios", id));
    }

    [HttpPut("UpdateScenario")]
    public async Task<IActionResult> UpdateScenario([Required] [FromBody] TimeScenario scenario,
        [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        DtoTimeScenario? dtoScenario = await scenarioServices.UpdateId("TimeScenarios", scenario, id);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return BadRequest();
    }
}