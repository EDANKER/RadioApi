using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.LifeSessionAudio;

public interface ILifeSessionAudioController
{
    
}

[Route("/api/v1/[controller]")]
[ApiController]
public class LifeSessionAudioController : ControllerBase,  ILifeSessionAudioController
{
    
}