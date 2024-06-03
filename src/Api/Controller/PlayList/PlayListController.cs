using System.ComponentModel.DataAnnotations;
using Api.Model.RequestModel.Update.UpdatePlayList;
using Api.Model.ResponseModel.PlayList;
using Api.Services.PlayListServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.PlayList;

[ApiController]
[Route("api/v1/[controller]")]
public class PlayListController(IPlayListServices playListServices) : ControllerBase
{
    [HttpPost("CreatePlayList")]
    public async Task<IActionResult> CreatePlayList([Required] IFormFile formFile, [Required] [FromQuery] string name,
        [Required] [FromQuery] string description)
    {
        if (formFile.ContentType != "image/jpeg")
            return BadRequest("Это не фото");
        if (await playListServices.Search("PlayLists", name, "Name"))
            return BadRequest("Такие данные уже есть");

        return Ok(await playListServices.CreateOrSave("PlayLists", name, description, formFile));
    }

    [HttpPut("UpdatePlayList")]
    public async Task<IActionResult> UpdatePlayList([FromBody] UpdatePlayList updatePlayList, [Required] [FromQuery] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id < 0)
            return BadRequest("Некорректное значение id");

        return Ok(await playListServices.UpdateId("PlayLists", updatePlayList, id));
    }

    [HttpDelete("DeletePlayList")]
    public async Task<IActionResult> DeletePlayList([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");

        return Ok(await playListServices.DeleteId("PlayLists", id));
    }

    [HttpGet("GetPlayListId")]
    public async Task<IActionResult> GetPlayListId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        DtoPlayList? dtoPlayList = await playListServices.GetId("PlayLists", id, true);
        if (dtoPlayList != null)
            return Ok(dtoPlayList);

        return Content("status 204");
    }

    [HttpGet("GetPlayListLimit")]
    public async Task<IActionResult> GetPlayListLimit([Required] [FromQuery] int limit)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение limit");
        List<DtoPlayList>? dtoPlayList = await playListServices.GetLimit("PlayLists", limit);
        if (dtoPlayList != null)
            return Ok(dtoPlayList);

        return Content("status 204");
    }
}