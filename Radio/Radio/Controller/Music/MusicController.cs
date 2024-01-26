using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
using Radio.Services.MusicServices;

namespace Radio.Controller.Music;

public interface IMusicController
{
    public Task<IActionResult> SaveMusic(Model.Music.Music music);
    public Task<IActionResult> GetMusic(int limit);
    public Task<IActionResult> GetMusic(string name);
    public Task<IActionResult> DeleteMusic(string name);
}

[Route("api/v1/[controller]")]
[ApiController]
public class MusicController : ControllerBase, IMusicController
{
    private IMusicServices _music;

    public MusicController(IMusicServices music)
    {
        _music = music;
    }

    [HttpPost("[action]")]
    public Task<IActionResult> SaveMusic(Model.Music.Music music)
    {
        throw new NotImplementedException();
    }

    [HttpGet("[action]{limit:int}")]
    public async Task<IActionResult> GetMusic(int limit)
    {
        return Ok(_music.GetMusic(limit));
    }

    [HttpGet("[action]{name}")]
    public Task<IActionResult> GetMusic(string name)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("[action]{name}")]
    public Task<IActionResult> DeleteMusic(string name)
    {
        throw new NotImplementedException();
    }
}