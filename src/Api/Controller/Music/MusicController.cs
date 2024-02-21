using Api.Services.MusicPlayerToMicroControllerServices;
using Api.Services.MusicServices;
using Api.Services.PlayListServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Music;

public interface IMusicController
{
    public Task<IActionResult> PlayMusic(string path,  string[] florSector);
    public Task<IActionResult> StopMusic();
    public Task<IActionResult> SaveMusic(IFormFile formFile, string name);
    public Task<IActionResult> GetMusicLimit(int limit);
    
    public Task<IActionResult> GetMusic(int id);
    public Task<IActionResult> DeleteMusicId(int id, string path);
    public Task<IActionResult> Update(string name, string field, int id);
    public Task<IActionResult> GetMusicPlayListTag(string name);
}

[Route("api/v1/[controller]")]
[ApiController]
public class MusicController(IPlayListServices playListServices,IMusicServices musicServices, IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices)
    : ControllerBase, IMusicController
{
    [HttpPost("PlayMusic")]
    public async Task<IActionResult> PlayMusic([FromHeader] string path, [FromBody] string[] florSector)
    {
        return Ok(await musicPlayerToMicroControllerServices.Play(path, florSector));
    }

    [HttpPost("StopMusic")]
    public async Task<IActionResult> StopMusic()
    {
        return Ok(await musicPlayerToMicroControllerServices.Stop());
    }

    [HttpPost("SaveMusic")]
    public async Task<IActionResult> SaveMusic(IFormFile formFile, [FromHeader]string name)
    {
        if (formFile == null || await musicServices.Search("Musics", formFile.FileName))
            return BadRequest("Такие данные уже есть или данные пусты");

        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");
        if (!await playListServices.Search("PlayLists", name))
            return BadRequest("Такого плей листа нет");

        return Ok(await musicServices.CreateOrSave("Musics", formFile, name));
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
    public async Task<IActionResult> DeleteMusicId(int id, [FromBody] string path)
    {
        return Ok(await musicServices.DeleteId("Musics", id, path));
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update([FromBody] string name,[FromHeader] string field, [FromHeader] int id)
    {
        return Ok(await musicServices.Update("Musics", field, name, id));
    }

    [HttpGet("GetPlayListTag")]
    public async Task<IActionResult> GetMusicPlayListTag(string name)
    {
        return Ok(await musicServices.GetMusicPlayListTag("Musics", name));
    }
}