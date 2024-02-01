using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radio.Data.Repository;
using Radio.Model.User;


namespace Radio.Controller.AdminPanel.AdminPanelSettings;

public interface IAdminPanelSettingsController
{
    public Task<IActionResult> CreateNewUser(User user);
    public Task<IActionResult> DeleteUser(string name);
    public Task<IActionResult> UpdateUser(User user);
    public Task<IActionResult> GetUser(int limit);
    public Task<IActionResult> GetNameUser(string name);
}
[Authorize(Roles = "Admin")]
[Route("api/v1/[controller]")]
[ApiController]
public class AdminPanelSettingsController : ControllerBase, IAdminPanelSettingsController
{
    private IUserRepository _repository;

    public AdminPanelSettingsController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateNewUser(User user)
    {
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteUser(string name)
    {
        return Ok();
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateUser(User user)
    {
        return Ok();
    }

    [HttpGet("[action]/{limit:int}")]
    public Task<IActionResult> GetUser(int limit)
    {
        throw new NotImplementedException();
    }

    [HttpGet("[action]/{name}")]
    public Task<IActionResult> GetNameUser(string name)
    {
        throw new NotImplementedException();
    }
}