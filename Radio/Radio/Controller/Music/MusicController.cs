using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
using Radio.Data.Repository;
using Radio.Services.MusicServices;

namespace Radio.Controller.Music;

public interface IMusicController
{
    public Task<IActionResult> SaveMusic(Model.RequestModel.Music.Music music);
    public Task<IActionResult> GetMusicLimit(int limit);
    public Task<IActionResult> GetMusic(int id);
    public Task<IActionResult> DeleteMusic(int id);
    public Task<IActionResult> Update(string name);
}

[Route("api/v1/[controller]")]
[ApiController]
public class MusicController : ControllerBase, IMusicController
{
    private IMusicServices _music;
    private IMusicRepository _musicRepository;

    public MusicController(IMusicServices music, IMusicRepository musicRepository)
    {
        _music = music;
        _musicRepository = musicRepository;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SaveMusic(Model.RequestModel.Music.Music music)
    {
        return Ok(_musicRepository.CreateOrSave("Music", music));
    }

    [HttpGet("[action]/{limit:int}")]
    public async Task<IActionResult> GetMusicLimit(int limit)
    {
        return Ok(_music.GetMusic(limit));
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> GetMusic(int id)
    {
        return Ok(_musicRepository.GetId("Music",id));
    }

    [HttpDelete("[action]/{id:int}")]
    public async Task<IActionResult> DeleteMusic(int id)
    {
        return Ok(_musicRepository.DeleteName("Music",id));
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update(string name)
    {
       return Ok(_musicRepository.Update("Music",name));
    }
}