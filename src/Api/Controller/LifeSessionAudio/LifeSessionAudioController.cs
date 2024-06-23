using System.ComponentModel.DataAnnotations;
using Api.Services.MusicServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.LifeSessionAudio;

[Route("/api/v1/[controller]")]
[ApiController]
public class LifeSessionAudioController(IMusicServices musicServices)
    : ControllerBase
{
    [HttpPost("LifeSessionAudio")]
    public async Task<IActionResult> LifeSessionAudio([Required] IFormFile formFile, [Required] [FromQuery] int[] idController)
    {
        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");
        
        return Ok(await musicServices.PlayLife(formFile, idController));
    }
}