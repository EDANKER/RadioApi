using Api.Services.PlayListServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.PlayList;

public interface IPlayListController
{
    public Task<IActionResult> CreatePlayList(string name, string description, IFormFile formFile);
    public Task<IActionResult> UpdatePlayList(string field, string purpose, int id);
    public Task<IActionResult> DeletePlayList(int id);
    public Task<IActionResult> GetPlayListId(int id);
    public Task<IActionResult> GetPlayList(int limit);
}
[ApiController]
[Route("api/v1/[controller]")]

public class PlayListController(IPlayListServices playListServices) : ControllerBase, IPlayListController
{
    [HttpPost("CreatePlayList")]
    public async Task<IActionResult> CreatePlayList([FromHeader] string name,  [FromHeader] string description, IFormFile formFile)
    {
        if (formFile.FileName.Length == 0)
            return BadRequest("Данные пусты");
        if (formFile == null)
            return BadRequest("Данные пусты");
        if (name == null)
            return BadRequest("Данные пусты");
        if (description == null)
            return BadRequest("Данные пусты");
        if (await playListServices.Search("PlayLists", name))
            return BadRequest("Такие данные уже есть");
        if (formFile.ContentType != "image/jpeg" || 
            formFile.ContentType != "image/png")
            return BadRequest("Это не фото");
        
        return Ok(await playListServices.CreateOrSave("PlayLists", name, description, formFile));
    }

    [HttpPatch("UpdatePlayList/{id:int}")]
    public async Task<IActionResult> UpdatePlayList([FromHeader] string field, [FromHeader] string purpose, int id)
    {
        if (purpose == null)
            return BadRequest("Данные пусты");
        if (field == null)
            return BadRequest("Данные пусты");
        if (id <= 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await playListServices.Update("PlayLists", field, purpose, id));
    }

    [HttpDelete("DeletePlayList/{id:int}")]
    public async Task<IActionResult> DeletePlayList(int id)
    {
        if (id <= 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await playListServices.DeleteId("PlayLists", id));
    }

    [HttpGet("GetPlayListId/{id:int}")]
    public async Task<IActionResult> GetPlayListId(int id)
    {
        if (id <= 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await playListServices.GetPlayListId("PlayLists", id));
    }

    [HttpGet("GetPlayList/{limit:int}")]
    public async Task<IActionResult> GetPlayList(int limit)
    {
        if (limit <= 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await playListServices.GetPlayList("PlayLists", limit));
    }
}