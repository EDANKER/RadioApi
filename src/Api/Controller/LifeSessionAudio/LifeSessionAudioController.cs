using Api.Services.AudioFileSaveToMicroControllerServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.LifeSessionAudio;

public interface ILifeSessionAudioController
{
    public Task<IActionResult> LifeSessionAudio(IFormFile formFile, string[] florSector);
}

[Route("/api/v1/[controller]")]
[ApiController]
public class LifeSessionAudioController(IAudioFileSaveToMicroControllerServices audioFileSaveToMicroControllerServices)
    : ControllerBase, ILifeSessionAudioController
{
    [HttpPost("LifeSessionAudio")]
    public async Task<IActionResult> LifeSessionAudio(IFormFile formFile, string[] florSector)
    {
        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");
        
        return Ok(await audioFileSaveToMicroControllerServices.SaveThenPlay(formFile, florSector));
    }
}