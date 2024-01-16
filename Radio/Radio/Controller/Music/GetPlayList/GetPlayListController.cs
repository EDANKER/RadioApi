using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.MusicScript.GetPlayList;

public interface IGetPlayListController
{
    public Task<IActionResult> GetLimitPlayList(int limit);
    public Task<IActionResult> GetNamePlayList(string name);
}

[Route("/api/v1/[controller]")]
[ApiController]
public class GetPlayListController : ControllerBase, IGetPlayListController
{
    [HttpGet("[action]{limit:int}")]
    public Task<IActionResult> GetLimitPlayList(int limit)
    {
        throw new NotImplementedException();
    }

    [HttpGet("[action]{name}")]
    public Task<IActionResult> GetNamePlayList(string name)
    {
        throw new NotImplementedException();
    }
}