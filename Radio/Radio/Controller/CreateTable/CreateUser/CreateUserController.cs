using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.CreateTable.CreateUser;

public interface ICreateUserController
{
    public Task<IActionResult> CreateUserTable();
}

public class CreateUserController : ControllerBase, ICreateUserController
{
    [HttpPost]
    public async Task<IActionResult> CreateUserTable()
    {
        throw new NotImplementedException();
    }
}