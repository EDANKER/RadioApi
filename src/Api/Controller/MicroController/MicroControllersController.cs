using Api.Model.ResponseModel.MicroController;
using Api.Services.MicroControllerServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.MicroController;

[ApiController]
[Route("api/v1/[controller]")]
public class MicroControllersController(IMicroControllerServices microControllerServices) : ControllerBase
{

    [HttpPost("CreateMicroController")]
    public async Task<IActionResult> CreateMicroController([FromBody]Model.RequestModel.MicroController.MicroController microController)
    {
        if (await microControllerServices.Search("MicroControllers", microController.Name, "Name")) 
            return BadRequest("Такой уже есть");
        
        return Ok(await microControllerServices.CreateOrSave("MicroControllers", microController));
    }
    
    [HttpGet("GetMicroControllerFloor/{floor:int}")]
    public async Task<IActionResult> GetMicroControllerFloor(int floor)
    {
        if (floor < 0)
            return BadRequest("Некорректное значение floor");
        List<DtoMicroController>? dtoMicroController = await microControllerServices.GetLimit("MicroControllers", floor);
        if (dtoMicroController != null)
            return Ok(dtoMicroController);
        
        return BadRequest("Таких данных нет");
    }

    [HttpGet("GetMicroControllerId/{id:int}")]
    public async Task<IActionResult> GetMicroControllerId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        DtoMicroController? dtoMicroController = await microControllerServices.GetId("MicroControllers", id);
        if (dtoMicroController != null)
            return Ok(dtoMicroController);
        
        return BadRequest("Таких данных нет");
    }

    [HttpDelete("DeleteMicroControllerId/{id:int}")]
    public async Task<IActionResult> DeleteMicroControllerId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await microControllerServices.DeleteId("MicroControllers", id));
    }
}