using Api.Model.RequestModel.Update.UpdateMusic;
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
    public async Task<IActionResult> SaveMusic(IFormFile formFile, [FromHeader]string namePlayList)
    {
        if (formFile == null)
            return BadRequest("Такие данные уже есть или данные пусты");
        if (await musicServices.Search("Musics", formFile.FileName, "Name"))
            return BadRequest("Такие данные уже есть или данные пусты");
        if (formFile.ContentType != "audio/mpeg")
            return BadRequest("Только audio/mpeg");

        return Ok(await musicServices.CreateOrSave("Musics", formFile, namePlayList));
    }

    [HttpGet("GetMusicLimit/{limit:int}")]
    public async Task<IActionResult> GetMusicLimit(int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение limit");
        List<DtoMusic>? dtoMusics = await musicServices.GetLimit("Musics", limit);
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

    [HttpPut("UpdateMusic/{id:int}")]
    public async Task<IActionResult> UpdateMusic([FromBody] UpdateMusic updateMusic, int id)
    {
        if (updateMusic == null)
            return BadRequest("Данные пусты");
        if (await musicServices.Search("Musics", updateMusic.Name, "Name"))
            return BadRequest("Такое имя уже занято");
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await musicServices.UpdateId("Musics", updateMusic, id));
    }

    [HttpGet("GetMusicPlayListTag")]
    public async Task<IActionResult> GetMusicPlayListTag(string name)
    {
        if (name == null)
            return BadRequest("Название не должно быть пустым");
        
        List<DtoMusic>? dtoMusic = await musicServices.GetUni("Musics", name, "NamePlayList");
        if (dtoMusic != null)
            return Ok(dtoMusic);
        
        return BadRequest("Таких данных нет");
    }
}