using Api.Services.MusicServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.LifeSessionAudio;

[Route("/api/v1/[controller]")]
[ApiController]
public class LifeSessionAudioController(IMusicServices musicServices)
    : ControllerBase
{
    [HttpPost("LifeSessionAudio")]
    public async Task<IActionResult> LifeSessionAudio(IFormFile formFile, string[] florSector)
    {
        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");
        
        return Ok(await musicServices.PlayLife(formFile, florSector));
    }
}