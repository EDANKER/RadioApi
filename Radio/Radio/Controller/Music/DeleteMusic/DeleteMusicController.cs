using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Music.DeleteMusic;

public interface IDeleteMusicController
{
    public Task<IActionResult> DeleteAllMusic();
    public Task<IActionResult> DeleteIdMusic();
}

public class DeleteMusicController : ControllerBase, IDeleteMusicController
{
    [HttpDelete("deleteAllMusic")]
    public Task<IActionResult> DeleteAllMusic()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("deleteIdMusic")]
    public Task<IActionResult> DeleteIdMusic()
    {
        throw new NotImplementedException();
    }
}