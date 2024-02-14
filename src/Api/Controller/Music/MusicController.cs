using Api.Services.MusicServices;
using Api.Services.TransmissionToMicroController;
using Microsoft.AspNetCore.Mvc;
using NAudio.Wave;

namespace Api.Controller.Music;

public interface IMusicController
{
    public Task<IActionResult> PlayMusic(string path);
    public Task<IActionResult> StopMusic();
    public Task<IActionResult> SaveMusic(IFormFile formFile, int id);
    public Task<IActionResult> GetMusicLimit(int limit);
    public Task<IActionResult> GetMusic(int id);
    public Task<IActionResult> DeleteMusicId(int id);
    public Task<IActionResult> Update(string name, string field, int id);
    public Task<IActionResult> GetPlayListTag(int id);
}

[Route("api/v1/[controller]")]
[ApiController]
public class MusicController(IMusicServices musicServices, IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices)
    : ControllerBase, IMusicController
{
    [HttpPost("PlayMusic")]
    public async Task<IActionResult> PlayMusic([FromBody] string path)
    {
        return Ok(await musicPlayerToMicroControllerServices.Play(path));
    }

    [HttpPost("StopMusic")]
    public async Task<IActionResult> StopMusic()
    {
        return Ok(await musicPlayerToMicroControllerServices.Stop());
    }

    [HttpPost("SaveMusic/{id:int}")]
    public async Task<IActionResult> SaveMusic(IFormFile formFile, int id)
    {
        if (formFile == null || await musicServices.Search("Musics", formFile.FileName))
            return BadRequest("Такие данные уже есть или данные пусты");

        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");

        return Ok(await musicServices.CreateOrSave("Musics", formFile, id));
    }

    [HttpGet("GetMusicLimit/{limit:int}")]
    public async Task<IActionResult> GetMusicLimit(int limit)
    {
        return Ok(await musicServices.GetMusic("Musics", limit));
    }

    [HttpGet("GetMusic/{id:int}")]
    public async Task<IActionResult> GetMusic(int id)
    {
        return Ok(await musicServices.GetMusicId("Musics", id));
    }

    [HttpDelete("DeleteMusicId/{id:int}")]
    public async Task<IActionResult> DeleteMusicId(int id)
    {
        return Ok(await musicServices.DeleteId("Musics", id));
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update([FromBody] string name, [FromHeader] string field, [FromHeader] int id)
    {
        return Ok(await musicServices.Update("Musics", field, name, id));
    }

    [HttpGet("GetPlayListTag/{id:int}")]
    public async Task<IActionResult> GetPlayListTag(int id)
    {
        return Ok(await musicServices.GetMusicTagPlayList("Musics", id));
    }
}