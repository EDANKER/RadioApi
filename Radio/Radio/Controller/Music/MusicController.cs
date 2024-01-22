using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Music;

public interface IMusicController
{
    public Task<IActionResult> SaveMusic(Model.User.Music music);
    public Task<IActionResult> DeleteAllMusic();
    public Task<IActionResult> DeleteIdMusic(string name);
}

[Route("api/v1/[controller]")]
[ApiController]
public class MusicController : IMusicController
{
    [HttpPost("[action]")]
    public Task<IActionResult> SaveMusic(Model.User.Music music)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("[action]")]
    public Task<IActionResult> DeleteAllMusic()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("[action]{name}")]
    public Task<IActionResult> DeleteIdMusic(string name)
    {
        throw new NotImplementedException();
    }
}