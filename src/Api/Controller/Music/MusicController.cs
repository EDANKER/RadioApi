using Api.Services.MusicServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Music;

public interface IMusicController
{
    public Task<IActionResult> PlayMusic(int idMusic,  int cabinet, int flor);
    public Task<IActionResult> StopMusic();
    public Task<IActionResult> SaveMusic(IFormFile formFile, string name);
    public Task<IActionResult> GetMusicLimit(int limit);
    public Task<IActionResult> GetMusic(int id);
    public Task<IActionResult> DeleteMusicId(int id, string path);
    public Task<IActionResult> Update(string name, string path, string field, int id);
    public Task<IActionResult> GetMusicPlayListTag(string name);
}

[Route("api/v1/[controller]")]
[ApiController]
public class MusicController(IMusicServices musicServices)
    : ControllerBase, IMusicController
{
    [HttpPost("PlayMusic/{idMusic:int}")]
    public async Task<IActionResult> PlayMusic(int idMusic, [FromHeader] int cabinet, [FromHeader] int floor)
    {
        if (idMusic == null)
            return BadRequest("Путь не должен быть пустой");
        if (floor == null)
            return BadRequest("Запуск по секторам не должен быть пуст");
        if (cabinet == null)
            return BadRequest("Запуск по секторам не должен быть пуст");
        
        return Ok(await musicServices.Play(idMusic, cabinet, floor));
    }

    [HttpPost("StopMusic")]
    public async Task<IActionResult> StopMusic()
    {
        return Ok(await musicServices.Stop());
    }

    [HttpPost("SaveMusic")]
    public async Task<IActionResult> SaveMusic(IFormFile formFile, [FromHeader]string name)
    {
        if (formFile == null)
            return BadRequest("Такие данные уже есть или данные пусты");
        if (await musicServices.Search("Musics", formFile.FileName))
            return BadRequest("Такие данные уже есть или данные пусты");
        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");
        if (name == "All")
            return Ok(await musicServices.CreateOrSave("Musics", formFile, "All"));

        return Ok(await musicServices.CreateOrSave("Musics", formFile, name));
    }

    [HttpGet("GetMusicLimit/{limit:int}")]
    public async Task<IActionResult> GetMusicLimit(int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await musicServices.GetMusic("Musics", limit));
    }

    [HttpGet("GetMusic/{id:int}")]
    public async Task<IActionResult> GetMusic(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await musicServices.GetMusicId("Musics", id));
    }

    [HttpDelete("DeleteMusicId/{id:int}")]
    public async Task<IActionResult> DeleteMusicId(int id, [FromBody] string path)
    {
        if (path == null)
            return BadRequest("Путь не должен быть пустой");
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await musicServices.DeleteId("Musics", id, path));
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update([FromHeader] string path, [FromHeader] string name,[FromHeader] string field, [FromHeader] int id)
    {
        if (name == null)
            return BadRequest("Данные пусты");
        if (field == null)
            return BadRequest("Данные пусты");   
        if (path == null)
            return BadRequest("Данные пусты");
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await musicServices.Update("Musics", path, field, name + ".mp3", id));
    }

    [HttpGet("GetPlayListTag")]
    public async Task<IActionResult> GetMusicPlayListTag(string name)
    {
        if (name == null)
            return BadRequest("Название не должно быть пустой");
        
        return Ok(await musicServices.GetMusicPlayListTag("Musics", name));
    }
}