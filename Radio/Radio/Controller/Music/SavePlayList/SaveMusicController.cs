using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.MusicScript.SavePlayList;

public interface ISaveMusicController
{
    public Task<IActionResult> SaveMusic();
}

[Route("/api/v1/[controller]")]
[ApiController]
public class SaveMusicController : ISaveMusicController
{
    [HttpPost("saveMusic")]
    public Task<IActionResult> SaveMusic()
    {
        throw new NotImplementedException();
    }
}