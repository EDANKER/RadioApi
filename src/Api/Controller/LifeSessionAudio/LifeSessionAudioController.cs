using Api.Services.AudioFileSaveToMicroControllerServices;
using Api.Services.MusicServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.LifeSessionAudio;

public interface ILifeSessionAudioController
{
    public Task<IActionResult> LifeSessionAudio(IFormFile formFile, List<string> florSector);
}

[Route("/api/v1/[controller]")]
[ApiController]
public class LifeSessionAudioController(IMusicServices musicServices)
    : ControllerBase, ILifeSessionAudioController
{
    [HttpPost("LifeSessionAudio")]
    public async Task<IActionResult> LifeSessionAudio(IFormFile formFile, List<string> florSector)
    {
        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");
        
        return Ok(await musicServices.PlayLife(formFile, florSector));
    }
}