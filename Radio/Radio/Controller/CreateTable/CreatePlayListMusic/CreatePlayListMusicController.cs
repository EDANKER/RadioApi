using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.CreateTable.CreatePlayListMusic;

public interface ICreatePlayListMusicController
{
    public Task<IActionResult> CreatePlayListTable();
}

public class CreatePlayListMusicController : ControllerBase, ICreatePlayListMusicController
{
    [HttpPost]
    public async Task<IActionResult> CreatePlayListTable()
    {
        throw new NotImplementedException();
    }
}