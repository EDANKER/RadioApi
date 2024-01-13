using Microsoft.AspNetCore.Mvc;
using Radio.Model;

namespace Radio.Controller.Admin;

public interface IAdminSettingsController
{
    public Task<IActionResult> CreateNewUser(User user);
    public Task<IActionResult> DeleteUser(int id);
    public Task<IActionResult> UpdateUser(User user);
}

[Route("api/v1/[controller]")]
[ApiController]
public class AdminSettingsController : ControllerBase, IAdminSettingsController
{

    [HttpPost("createNewUser")]
    public Task<IActionResult> CreateNewUser(User user)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("deleteUser")]
    public Task<IActionResult> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut("updateUser")]
    public Task<IActionResult> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}