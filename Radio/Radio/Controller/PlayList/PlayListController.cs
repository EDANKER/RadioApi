using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.PlayList;

public interface IPlayListController
{
    public Task<IActionResult> CreatePlayList(Model.User.PlayList playList);
    public Task<IActionResult> UpdateNamePlayList(string name);
    public Task<IActionResult> DeletePlayList(string name);
}

[Route("api/v1/[controller]")]
[ApiController]
public class PlayListController : IPlayListController
{
    [HttpPost("[action]")]
    public Task<IActionResult> CreatePlayList(Model.User.PlayList playList)
    {
        throw new NotImplementedException();
    }

    [HttpPut("[action]")]
    public Task<IActionResult> UpdateNamePlayList(string name)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("[action]")]
    public Task<IActionResult> DeletePlayList(string name)
    {
        throw new NotImplementedException();
    }
}