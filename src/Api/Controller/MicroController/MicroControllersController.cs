using Api.Services.MicroControllerServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.MicroController;

[ApiController]
[Route("api/v1/[controller]")]
public class MicroControllersController(IMicroControllerServices microControllerServices) : ControllerBase
{
    [HttpGet("GetMusicInMinio/{id:int}")]
    public async Task<IActionResult> GetMusicInMinio(int id)
    {
        byte[]? buffer = await microControllerServices.GetMusicInMinio(id);
        
        while (buffer?.Length > 0)
            return Ok(await microControllerServices.GetMusicInMinio(id));
        
        return BadRequest("Это все или таких данных нет");
    }

    [HttpPost("CreateMicroController")]
    public async Task<IActionResult> CreateMicroController([FromBody]Model.RequestModel.MicroController.MicroController microController)
    {
        if (await microControllerServices.Search("MicroControllers", microController.Name)) 
            return BadRequest("Такой уже есть");
        
        return Ok(await microControllerServices.CreateOrSave("MicroControllers", microController));
    }
    
    [HttpGet("GetMicroController/{limit:int}")]
    public async Task<IActionResult> GetMicroControllerLimit(int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await microControllerServices.GetLimit("MicroControllers", limit));
    }

    [HttpGet("GetMicroControllerId/{id:int}")]
    public async Task<IActionResult> GetMicroControllerId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await microControllerServices.GetId("MicroControllers", id));
    }

    [HttpDelete("DeleteMicroControllerId/{id:int}")]
    public async Task<IActionResult> DeleteMicroControllerId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await microControllerServices.DeleteId("MicroControllers", id));
    }
}