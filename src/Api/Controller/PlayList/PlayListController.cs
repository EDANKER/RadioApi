﻿using Api.Services.PlayListServices;
using Microsoft.AspNetCore.Mvc;
using Radio.Data.Repository.PlayList;

namespace Radio.Controller.PlayList;

public interface IPlayListController
{
    public Task<IActionResult> CreatePlayList(Model.PlayList.PlayList playList);
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
    public async Task<IActionResult> CreatePlayList([FromBody] Model.PlayList.PlayList playList)
    {
        if (await playListServices.Search("PlayLists", playList.Name))
        {
            return BadRequest("Такие данные уже есть или данные пусты");
        }
        
        return Ok(await playListServices.CreateOrSave("PlayLists", playList));
    }

    [HttpPatch("UpdatePlayList/{id:int}")]
    public async Task<IActionResult> UpdatePlayList([FromHeader] string field, [FromBody] string purpose, int id)
    {
        return Ok(await playListServices.Update("PlayLists", purpose, field, id));
    }

    [HttpDelete("DeletePlayList/{id:int}")]
    public async Task<IActionResult> DeletePlayList(int id)
    {
        return Ok(await playListServices.DeleteId("PlayLists", id));
    }

    [HttpGet("GetPlayListId/{id:int}")]
    public async Task<IActionResult> GetPlayListId(int id)
    {
        return Ok(await playListServices.GetPlayListId("PlayLists", id));
    }

    [HttpGet("GetPlayList/{limit:int}")]
    public async Task<IActionResult> GetPlayList(int limit)
    {
        return Ok(await playListServices.GetPlayList("PlayLists", limit));
    }
}