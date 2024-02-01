using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radio.Data.Repository;
using Radio.Services.PlayListServices;

namespace Radio.Controller.PlayList;

public interface IPlayListController
{
    public Task<IActionResult> CreatePlayList(Model.PlayList.PlayList playList);
    public Task<IActionResult> UpdatePlayList(string purpose, int id);
    public Task<IActionResult> DeletePlayList(int id);
    public Task<IActionResult> GetPlayListId(int id);
    public Task<IActionResult> GetPlayList(int limit);
}
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]

public class PlayListController : ControllerBase, IPlayListController
{
    private IPlayListServices _playListServices;
    private IPlayListRepository _repository;

    public PlayListController(IPlayListServices playListServices, IPlayListRepository repository)
    {
        _playListServices = playListServices;
        _repository = repository;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreatePlayList([FromBody] Model.PlayList.PlayList playList)
    {
        return Ok(_repository.CreateOrSave("PlayList", playList));
    }

    [HttpPut("[action]/{id:int}")]
    public async Task<IActionResult> UpdatePlayList([FromBody] string purpose, [FromHeader]int id)
    {
        return Ok(_repository.Update("PlayList", purpose, id));
    }

    [HttpDelete("[action]/{id:int}")]
    public async Task<IActionResult> DeletePlayList(int id)
    {
        return Ok(_repository.DeleteId("PlayList", id));
    }

    [HttpGet("GetPlayListId/{id:int}")]
    public async Task<IActionResult> GetPlayListId(int id)
    {
        return Ok(_playListServices.GetPlayListId(id));
    }

    [HttpGet("[action]/{limit:int}")]
    public async Task<IActionResult> GetPlayList(int limit)
    {
        return Ok(_playListServices.GetPlayList(limit));
    }

    [HttpGet("Hi")]
    public async Task<IActionResult> Get()
    {
        return Ok("Heelo");
    }
}