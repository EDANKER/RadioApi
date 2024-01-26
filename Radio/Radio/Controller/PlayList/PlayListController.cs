using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Radio.Data.Repository;
using Radio.Services.PlayListServices;

namespace Radio.Controller.PlayList;

public interface IPlayListController
{
    public Task<IActionResult> CreatePlayList(Model.PlayList.PlayList playList);
    public Task<IActionResult> UpdatePlayList(string name);
    public Task<IActionResult> DeletePlayList(string name);
    public Task<IActionResult> GetPlayListId(int id);
    public Task<IActionResult> GetPlayList(int limit);
}

[Route("api/v1/[controller]")]
[ApiController]
public class PlayListController : ControllerBase, IPlayListController
{
    private IPlayListServices _playListServices;

    private string _connect;

    private IPlayListRepository _repository;

    public PlayListController(IConfiguration configuration,IPlayListServices playListServices, IPlayListRepository repository)
    {
        _playListServices = playListServices;
        _repository = repository;
        _connect = configuration.GetConnectionString("MySqL");
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreatePlayList(Model.PlayList.PlayList playList)
    {
        return Ok(_repository.CreateOrSave("PlayList", playList));
    }

    [HttpPut("[action]")]
    public Task<IActionResult> UpdatePlayList(string name)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeletePlayList(string name)
    {
        return Ok();
    }

    [HttpGet("[action]{id:int}")]
    public async Task<IActionResult> GetPlayListId(int id)
    {
        return Ok(_playListServices.GetPlayId(id));
    }

    [HttpGet("[action]{limit:int}")]
    public async Task<IActionResult> GetPlayList(int limit)
    {
        return Ok(_playListServices.GetPlayList(limit));
    }
}