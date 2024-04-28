using System.ComponentModel.DataAnnotations;
using Api.Services.MusicServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controller.LifeSessionAudio;

[Route("/api/v1/[controller]")]
[ApiController]
public class LifeSessionAudioController(IMusicServices musicServices)
    : ControllerBase
{
    [HttpPost("LifeSessionAudio")]
    public async Task<IActionResult> LifeSessionAudio([Required] [FromForm] IFormFile formFile, [Required] [FromHeader] int[] idController)
    {
        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");
        
        return Ok(await musicServices.PlayLife(formFile, idController));
    }
}