using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.CreateTable.CreateUser;

public interface ICreateUserController
{
    public Task<IActionResult> CreateUserTable();
}

public class CreateUserController : ControllerBase, ICreateUserController
{
    private string _connect;

    public CreateUserController(IConfiguration configuration)
    {
        _connect = configuration.GetConnectionString("PostGre");
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserTable()
    {
        const string command = "CREATE TABLE IF NOT EXISTS User" +
                               "(name VARCHAR(255) PRIMARY KEY," +
                               "login VARCHAR(255), " +
                               "speak BIT, " +
                               "settingsTime BIT, " +
                               "turnItOnMusic BIT)";

        
        return Ok();
    }
}