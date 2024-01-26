using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.CreateTable.CreatePlayListMusic;

public interface ICreatePlayListMusicController
{
    public Task<IActionResult> CreatePlayListTable();
}

public class CreatePlayListMusicController : ControllerBase, ICreatePlayListMusicController
{
    private string _connect;

    public CreatePlayListMusicController(IConfiguration configuration)
    {
        _connect = configuration.GetConnectionString("PostGre");
    }

    public async Task<IActionResult> CreatePlayListTable()
    {
        const string command = "CREATE TABLE PlayList" +
                               "(name VARCHAR(255), " +
                               "imgPath VARCHAR(255))";

        return Ok();
    }
}