﻿using System.ComponentModel.DataAnnotations;
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
    [HttpPost("PlayMusic")]
    public async Task<IActionResult> PlayMusic([Required] [FromQuery] int idMusic, [Required] [FromBody] int[] idControllers)
    {
        return Ok(await musicServices.Play(idMusic, idControllers));
    }

    [HttpPost("StopMusic")]
    public async Task<IActionResult> StopMusic([Required] [FromBody] int[] idController)
    {
        return Ok(await musicServices.Stop(idController));
    }

    [HttpPost("SaveMusic")]
    public async Task<IActionResult> SaveMusic([Required] IFormFile formFile, [Required] [FromQuery] string namePlayList)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (await musicServices.Search("Musics", formFile.FileName, "Name"))
            return BadRequest("Такие данные уже есть или данные пусты");
        
        DtoMusic? dtoMusic = await musicServices.CreateOrSave("Musics", formFile, namePlayList);
        if (dtoMusic != null)
            return Ok(dtoMusic);
        return Content("status 204");
    }

    [HttpGet("GetMusicLimit")]
    public async Task<IActionResult> GetMusicLimit([Required] [FromQuery] int limit, [Required] [FromQuery] int currentPage)
    {
        if (limit < 0)
            return BadRequest("Некорректное значение limit");
        List<DtoMusic>? dtoMusics = await musicServices.GetLimit("Musics", currentPage, limit);
        if (dtoMusics != null)
        {
            var response = new
            {
                Head = await musicServices.GetCountPage("Musics", currentPage, limit),
                Body = dtoMusics
            };
            return Ok(response);
        }
        
        return Content("status 204");
    }

    [HttpGet("GetMusicId")]
    public async Task<IActionResult> GetMusicId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        DtoMusic? dtoMusic = await musicServices.GetId("Musics", id);
        if (dtoMusic != null)
            return Ok(dtoMusic);
        
        return Content("status 204");
    }

    [HttpDelete("DeleteMusicId")]
    public async Task<IActionResult> DeleteMusicId([Required] [FromQuery] int id)
    {
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        return Ok(await musicServices.DeleteId("Musics", id));
    }

    [HttpPut("UpdateMusic")]
    public async Task<IActionResult> UpdateMusic([FromBody] UpdateMusic updateMusic, [Required] [FromQuery] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (await musicServices.Search("Musics", updateMusic.Name, "Name"))
            return BadRequest("Такое имя уже занято");
        if (id < 0)
            return BadRequest("Некорректное значение id");
        
        DtoMusic? dtoMusic = await musicServices.UpdateId("Musics", updateMusic, id);
        if (dtoMusic != null)
            return Ok(dtoMusic);
        return Content("status 204");
    }

    [HttpGet("GetMusicPlayListTag")]
    public async Task<IActionResult> GetMusicPlayListTag([Required] [FromQuery] string name)
    {
        DtoMusic? dtoMusic = await musicServices.GetField("Musics", name, "NamePlayList");
        if (dtoMusic != null)
            return Ok(dtoMusic);

        return Content("status 204");
    }
}