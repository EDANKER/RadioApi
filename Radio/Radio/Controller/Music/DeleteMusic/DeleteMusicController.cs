using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Music.DeleteMusic;

public interface IDeleteMusicController
{
    public Task<IActionResult> DeleteAllMusic();
    public Task<IActionResult> DeleteIdMusic(int id);
}

[Route("/api/v1/[controller]")]
[ApiController]
public class DeleteMusicController : ControllerBase, IDeleteMusicController
{
    [HttpDelete("deleteAllMusic")]
    public Task<IActionResult> DeleteAllMusic()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("deleteIdMusic{id:int}")]
    public Task<IActionResult> DeleteIdMusic(int id)
    {
        throw new NotImplementedException();
    }
}