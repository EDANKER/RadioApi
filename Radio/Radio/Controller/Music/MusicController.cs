using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
using NAudio.Wave;
using Radio.Data.Repository;
using Radio.Services.MusicServices;

namespace Radio.Controller.Music;

public interface IMusicController
{
    public Task<IActionResult> PlayMusic(string path);
    public Task<IActionResult> SaveMusic(IFormFile formFile, int id);
    public Task<IActionResult> GetMusicLimit(int limit);
    public Task<IActionResult> GetMusic(int id);
    public Task<IActionResult> DeleteMusic(int id);
    public Task<IActionResult> Update(string name);
    public Task<IActionResult> GetPlayListTag(int id);

}

[Route("api/v1/[controller]")]
[ApiController]
public class MusicController : ControllerBase, IMusicController
{
    private IMusicServices _musicServices;
    private IMusicRepository _musicRepository;

    public MusicController(IMusicServices musicServices, IMusicRepository musicRepository)
    {
        _musicServices = musicServices;
        _musicRepository = musicRepository;
    }

    [HttpPost("PlayMusic")]
    public async Task<IActionResult> PlayMusic(string path)
    {
        AudioFileReader audioFileReader = new AudioFileReader(path);
        WaveOutEvent waveOutEvent = new WaveOutEvent();
        
        waveOutEvent.Init(audioFileReader);
        waveOutEvent.Play();

        return Ok();
    }

    [HttpPost("[action]/{id:int}")]
    public async Task<IActionResult> SaveMusic(IFormFile formFile, int id)
    {
        if (formFile == null || await _musicRepository.Search("Musics", formFile.FileName))
        {
            return BadRequest("Такие данные уже есть или данные пусты");
        }

        string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Uploads/Music");
        string filePath = Path.Combine(uploadsPath, formFile.FileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Create);

        Model.RequestModel.Music.Music music = new Model.RequestModel.Music.Music(formFile.FileName, "Data/Uploads/Music/" + formFile.FileName, id);
        await formFile.CopyToAsync(fileStream);

        return Ok(await _musicRepository.CreateOrSave("Musics", music));
    }

    [HttpGet("[action]Limit/{limit:int}")]
    public async Task<IActionResult> GetMusicLimit(int limit)
    {
        return Ok(await _musicServices.GetMusic("Musics",limit));
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> GetMusic(int id)
    {
        return Ok(await _musicServices.GetMusicId("Musics", id));
    }

    [HttpDelete("[action]/{id:int}")]
    public async Task<IActionResult> DeleteMusic(int id)
    {
        return Ok(await _musicRepository.DeleteId("Musics", id));
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update(string name)
    {
        return Ok(await _musicRepository.Update("Musics", name));
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetPlayListTag(int id)
    {
        return Ok(await _musicServices.GetMusicTagPlayList("Musics", id));
    }
}