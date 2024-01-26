using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.CreateTable.CreateMusic;

public interface ICreateMusicController
{
    public Task<IActionResult> CreateMusicTable();
}

public class CreateMusicController : ControllerBase, ICreateMusicController
{
    private string _connect;
    public CreateMusicController(IConfiguration configuration)
    {
        _connect = configuration.GetConnectionString("PostGre");
    }

    [HttpPost]
    public async Task<IActionResult> CreateMusicTable()
    {
        const string command = "CREATE TABLE IF NOT EXISTS Music(" +
                               "name VARCHAR(255) PRIMARY KEY, " +
                               "namePlayList VARCHAR(255), " +
                               "path TEXT)";

        

        return Ok();
    }
}