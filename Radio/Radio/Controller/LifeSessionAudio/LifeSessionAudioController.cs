using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NAudio.Wave;

namespace Radio.Controller.LifeSessionAudio;

public interface ILifeSessionAudioController
{
    public Task<IActionResult> LifeSessionAudio(IFormFile formFile);
}

[Route("/api/v1/[controller]")]
[ApiController]
public class LifeSessionAudioController : ControllerBase, ILifeSessionAudioController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> LifeSessionAudio(IFormFile formFile)
    {
        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Uploads/Music");
        string filePath = Path.Combine(uploadPath, formFile.FileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await formFile.CopyToAsync(fileStream);

        AudioFileReader audioFileReader = new AudioFileReader("Data/Uploads/Music/" + formFile.FileName);
        WaveOutEvent waveOutEvent = new WaveOutEvent();
        
        waveOutEvent.Init(audioFileReader);
        waveOutEvent.Play();
        
        return Ok();
    }
}