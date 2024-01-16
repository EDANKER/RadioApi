using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.MusicScript.SavePlayList;

public interface ISaveMusicController
{
    public Task<IActionResult> SaveMusic();
}

[Route("/api/v1/[controller]")]
[ApiController]
public class SaveMusicController : ControllerBase, ISaveMusicController
{
    [HttpPost("saveMusic")]
    public async Task<IActionResult> SaveMusic()
    {
        return Ok();
    }
}