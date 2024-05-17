using System.ComponentModel.DataAnnotations;
using Api.Model.ResponseModel.MicroController;
using Api.Services.MicroControllerServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.MicroController;

[ApiController]
[Route("api/v1/[controller]")]
public class MicroControllersController(IMicroControllerServices microControllerServices) : ControllerBase
{
    
    [HttpPost("CreateMicroController")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateMicroController([FromBody]Model.RequestModel.MicroController.MicroController microController)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (await microControllerServices.Search("MicroControllers", microController.Name, "Name")) 
            return BadRequest("Такой уже есть");
        
        return Ok(await microControllerServices.CreateOrSave("MicroControllers", microController));
    }
    
    [HttpGet("GetMicroControllerFloor")]
    public async Task<IActionResult> GetMicroControllerFloor([Required] [FromQuery] int floor)
    {
        if (floor < 0)
            return BadRequest("Некорректное значение floor");
        List<DtoMicroController>? dtoMicroController = await microControllerServices.GetFloor("MicroControllers", floor);
        if (dtoMicroController != null)
            return Ok(dtoMicroController);
        
        return BadRequest("Таких данных нет");
    }

    [HttpPost("SoundVol")]
    public async Task<IActionResult> SoundVol([Required] [FromBody] int[] idMusicController, [FromQuery] [Required] int vol)
    {
        if (vol < 0)
            return BadRequest("Некорректное значение id");

        return Ok(await microControllerServices.SoundVol(idMusicController, vol));
    }

    [HttpGet("GetMicroControllerId")]
    public async Task<IActionResult> GetMicroControllerId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        DtoMicroController? dtoMicroController = await microControllerServices.GetId("MicroControllers", id);
        if (dtoMicroController != null)
            return Ok(dtoMicroController);
        
        return BadRequest("Таких данных нет");
    }

    [HttpPut("UpdateMicroController")]
    public async Task<IActionResult> UpdateMicroController([Required] [FromQuery] int id, [FromBody] Model.RequestModel.MicroController.MicroController microController)
    {
        return Ok(await microControllerServices.Update("MicroControllers" , microController, id));
    }

    [HttpDelete("DeleteMicroControllerId")]
    public async Task<IActionResult> DeleteMicroControllerId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await microControllerServices.DeleteId("MicroControllers", id));
    }
}