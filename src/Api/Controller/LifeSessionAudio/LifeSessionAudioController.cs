using Api.Services.LifeSessionAudioServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.LifeSessionAudio;

public interface ILifeSessionAudioController
{
    public Task<IActionResult> LifeSessionAudio(IFormFile formFile);
}

[Route("/api/v1/[controller]")]
[ApiController]
public class LifeSessionAudioController(ILifeSessionAudioServices lifeSessionAudioServices)
    : ControllerBase, ILifeSessionAudioController
{
    [HttpPost("LifeSessionAudio")]
    public async Task<IActionResult> LifeSessionAudio(IFormFile formFile)
    {
        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");
        
        return Ok(await lifeSessionAudioServices.Start(formFile));
    }
}