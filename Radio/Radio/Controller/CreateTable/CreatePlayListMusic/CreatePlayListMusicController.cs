using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

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

        NpgsqlConnection npgsqlConnection = new NpgsqlConnection(_connect);
        await npgsqlConnection.OpenAsync();
        
        NpgsqlCommand npgsqlCommand = new NpgsqlCommand(command, npgsqlConnection);

        await npgsqlCommand.ExecuteNonQueryAsync();
        await npgsqlConnection.CloseAsync();

        return Ok();
    }
}