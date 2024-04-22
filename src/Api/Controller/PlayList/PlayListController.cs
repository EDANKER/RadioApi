using Api.Model.ResponseModel.PlayList;
using Api.Services.PlayListServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.PlayList;

[ApiController]
[Route("api/v1/[controller]")]

public class PlayListController(IPlayListServices playListServices) : ControllerBase
{
    [HttpPost("CreatePlayList")]
    public async Task<IActionResult> CreatePlayList([FromHeader] string name,  [FromHeader] string description, IFormFile formFile)
    {
        if (formFile == null)
            return BadRequest("Данные пусты");
        if (name == null)
            return BadRequest("Данные пусты");
        if (description == null)
            return BadRequest("Данные пусты");
        if (await playListServices.Search("PlayLists", name, "Name"))
            return BadRequest("Такие данные уже есть");
        if (formFile.ContentType != "image/jpeg")
            return BadRequest("Это не фото");
        
        return Ok(await playListServices.CreateOrSave("PlayLists", name, description, formFile));
    }

    [HttpPut("UpdatePlayList/{id:int}")]
    public async Task<IActionResult> UpdatePlayList([FromBody] Model.RequestModel.PlayList.PlayList playList , int id)
    {
        if (playList == null)
            return BadRequest("Данные пусты");
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await playListServices.UpdateId("PlayLists", playList, id));
    }

    [HttpDelete("DeletePlayList/{id:int}")]
    public async Task<IActionResult> DeletePlayList(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await playListServices.DeleteId("PlayLists", id));
    }

    [HttpGet("GetPlayListId/{id:int}")]
    public async Task<IActionResult> GetPlayListId(int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        DtoPlayList? dtoPlayList = await playListServices.GetId("PlayLists", id, true);
        if (dtoPlayList != null)
            return Ok(dtoPlayList);

        return BadRequest("Таких данных нет");
    }

    [HttpGet("GetPlayListLimit/{limit:int}")]
    public async Task<IActionResult> GetPlayListLimit(int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение limit");
        List<DtoPlayList>? dtoPlayList = await playListServices.GetLimit("PlayLists", limit);
        if (dtoPlayList != null)
            return Ok(dtoPlayList);

        return BadRequest("Таких данных нет");
    }
}