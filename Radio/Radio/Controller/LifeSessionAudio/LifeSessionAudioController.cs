using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.LifeSessionAudio;

public interface ILifeSessionAudioController
{
    public Task<IActionResult> LifeSessionAudio();
}

[Route("/api/v1/[controller]")]
[ApiController]
public class LifeSessionAudioController : ControllerBase,  ILifeSessionAudioController
{
    public Task<IActionResult> LifeSessionAudio()
    {
        throw new NotImplementedException();
    }
}