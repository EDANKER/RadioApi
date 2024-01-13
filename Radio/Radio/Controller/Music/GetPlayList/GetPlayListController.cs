using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.MusicScript.GetPlayList;

public interface IGetPlayListController
{
    public Task<IActionResult> GetAllPlayList();
}

[Route("/api/v1/[controller]")]
[ApiController]
public class GetPlayListController : ControllerBase, IGetPlayListController
{
    [HttpGet("getAllPlayList")]
    public Task<IActionResult> GetAllPlayList()
    {
        throw new NotImplementedException();
    }
}