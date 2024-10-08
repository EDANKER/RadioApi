﻿using System.ComponentModel.DataAnnotations;
using Api.Model.RequestModel.Scenario.PlayScenario;
using Api.Model.ResponseModel.PlayScenario;
using Api.Services.ScenarioServices.ScenarioServicesPlay;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Scenario.ScenarioPlayController;

[ApiController]
[Route("api/v1/[controller]")]
public class ScenarioPlayController(IScenarioPlayServices scenarioServices) : ControllerBase
{
    [HttpPost("CreateOrSave")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateOrSave([FromBody] PlayScenario scenario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (await scenarioServices.Search("PlayScenarios", scenario.Name, "Name"))
            return BadRequest("Имя уже занято");
        
        DtoPlayScenario? dtoScenario = await scenarioServices.CreateOrSave("PlayMigrationsScenarios", scenario);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return BadRequest();
    }

    [HttpPost("PlayScenario")]
    public async Task<IActionResult> PlayScenario([Required] [FromQuery] int id)
    {
        return Ok(await scenarioServices.Play("PlayScenarios", id));
    }

    [HttpGet("GetScenarioLimit")]
    public async Task<IActionResult> GetScenarioLimit([Required] [FromQuery] int limit,
        [Required] [FromQuery] int currentPage)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение limit");
        List<DtoPlayScenario>? dtoScenario = await scenarioServices.GetLimit("PlayScenarios", currentPage, limit);
        if (dtoScenario != null)
        {
            var response = new
            {
                Head = await scenarioServices.GetCountPage("PlayScenarios", currentPage, limit),
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
        DtoPlayScenario? dtoScenario = await scenarioServices.GetId("PlayScenarios", id);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return Content("status 204");
    }

    [HttpDelete("DeleteScenarioId")]
    public async Task<IActionResult> DeleteScenarioId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");

        return Ok(await scenarioServices.DeleteId("PlayScenarios", id));
    }

    [HttpPut("UpdateScenario")]
    public async Task<IActionResult> UpdateScenario([Required] [FromBody] PlayScenario scenario,
        [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        DtoPlayScenario? dtoScenario = await scenarioServices.UpdateId("PlayScenarios", scenario, id);
        if (dtoScenario != null)
            return Ok(dtoScenario);

        return BadRequest();
    }
}