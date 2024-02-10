using Api.Services.MicroControllerServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.MicroController;

public interface IMicroController
{
    public Task<IActionResult> CreateMicroController(Model.RequestModel.MicroController.MicroController microController);
    public Task<IActionResult> GetMicroController();
}

[ApiController]
[Route("api/v1/[controller]")]
public class MicroControllersController(IMicroControllerServices microControllerServices) : ControllerBase, IMicroController
{

    [HttpPost("CreateMicroController")]
    public async Task<IActionResult> CreateMicroController([FromBody]Model.RequestModel.MicroController.MicroController microController)
    {
        if (await microControllerServices.Search("MicroControllers", microController.Name))
        {
           return Ok(await microControllerServices.CheckMicroController(microController.Ip));
        }
        
        return Ok(await microControllerServices.CreateOrSave("MicroControllers", microController));
    }
    
    [HttpGet("GetMicroController")]
    public async Task<IActionResult> GetMicroController()
    {
        return Ok(await microControllerServices.GetData("MicroControllers"));
    }
    
}