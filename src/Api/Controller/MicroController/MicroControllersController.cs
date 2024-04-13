using Api.Services.MicroControllerServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.MicroController;

public interface IMicroController
{ 
    Task<IActionResult> GetMusicInMinio(int id);
    Task<IActionResult> CreateMicroController(Model.RequestModel.MicroController.MicroController microController);
    Task<IActionResult> GetMicroController(int limit);
    Task<IActionResult> DeleteMicroControllerId(int id);
}

[ApiController]
[Route("api/v1/[controller]")]
public class MicroControllersController(IMicroControllerServices microControllerServices) : ControllerBase, IMicroController
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
    public async Task<IActionResult> GetMicroController(int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await microControllerServices.GetLimit("MicroControllers", limit));
    }

    [HttpDelete("DeleteMicroControllerId/{id:int}")]
    public async Task<IActionResult> DeleteMicroControllerId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await microControllerServices.DeleteId("MicroControllers", id));
    }
}