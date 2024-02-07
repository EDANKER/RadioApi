using Microsoft.AspNetCore.Mvc;
using Radio.Data.Repository.PlayList;
using Radio.Services.PlayListServices;

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

public class PlayListController : ControllerBase, IPlayListController
{
    private IPlayListServices _playListServices;
    private IPlayListRepository _repository;

    public PlayListController(IPlayListServices playListServices, IPlayListRepository repository)
    {
        _playListServices = playListServices;
        _repository = repository;
    }

    [HttpPost("CreatePlayList")]
    public async Task<IActionResult> CreatePlayList([FromBody] Model.PlayList.PlayList playList)
    {
        if (await _repository.Search("PlayList", playList.Name))
        {
            return BadRequest("Такие данные уже есть или данные пусты");
        }
        
        return Ok(await _repository.CreateOrSave("PlayList", playList));
    }

    [HttpPatch("UpdatePlayList/{id:int}")]
    public async Task<IActionResult> UpdatePlayList([FromHeader] string field, [FromBody] string purpose, int id)
    {
        return Ok(await _repository.Update("PlayList", purpose, field, id));
    }

    [HttpDelete("DeletePlayList/{id:int}")]
    public async Task<IActionResult> DeletePlayList(int id)
    {
        return Ok(await _repository.DeleteId("PlayList", id));
    }

    [HttpGet("GetPlayListId/{id:int}")]
    public async Task<IActionResult> GetPlayListId(int id)
    {
        return Ok(await _playListServices.GetPlayListId(id));
    }

    [HttpGet("GetPlayList/{limit:int}")]
    public async Task<IActionResult> GetPlayList(int limit)
    {
        return Ok(await _playListServices.GetPlayList(limit));
    }
}