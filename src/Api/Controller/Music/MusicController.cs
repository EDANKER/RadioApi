using Api.Model.ResponseModel.Music;
using Api.Services.MusicServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Music;

[Route("api/v1/[controller]")]
[ApiController]
public class MusicController(IMusicServices musicServices)
    : ControllerBase
{
    [HttpGet("GetMusicInMinio/{id:int}")]
    public async Task<IActionResult> GetMusicInMinio(int id)
    {
        byte[]? buffer = await musicServices.GetMusicInMinio(id);
        if (buffer == null)
            return BadRequest("Таких данных нет");
        
        return Ok(buffer);
    }
    
    [HttpPost("PlayMusic/{idMusic:int}")]
    public async Task<IActionResult> PlayMusic(int idMusic, [FromBody] int[] idControllers)
    {
        if (idMusic == null)
            return BadRequest("Путь не должен быть пустой");
        
        return Ok(await musicServices.Play(idMusic, idControllers));
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
            return BadRequest("Некорректное значение limit");
        List<DtoMusic>? dtoMusics = await musicServices.GetMusic("Musics", limit);
        if (dtoMusics != null)
            return Ok(dtoMusics);
        
        return BadRequest("Таких данных нет");
    }

    [HttpGet("GetMusicId/{id:int}")]
    public async Task<IActionResult> GetMusicId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        DtoMusic? dtoMusic = await musicServices.GetId("Musics", id);
        if (dtoMusic != null)
            return Ok(dtoMusic);
        
        return BadRequest("Таких данных нет");
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

    [HttpGet("GetMusicPlayListTag")]
    public async Task<IActionResult> GetMusicPlayListTag(string name)
    {
        if (name == null)
            return BadRequest("Название не должно быть пустым");
        
        List<DtoMusic>? dtoMusic = await musicServices.GetTag("Musics", name);
        if (dtoMusic != null)
            return Ok(dtoMusic);
        
        return BadRequest("Таких данных нет");
    }
}