using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Radio.Controller.CreateTable.CreatePlayListMusic;

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

        NpgsqlConnection npgsqlConnection = new NpgsqlConnection(_connect);
        await npgsqlConnection.OpenAsync();
        
        NpgsqlCommand npgsqlCommand = new NpgsqlCommand(command, npgsqlConnection);
        
        await npgsqlCommand.ExecuteNonQueryAsync();
        await npgsqlConnection.CloseAsync();

        return Ok();
    }
}