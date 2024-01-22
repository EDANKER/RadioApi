using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radio.Model;
using Radio.Model.User;
using Radio.Repository.Repository;

namespace Radio.Controller.Admin;

public interface IAdminPanelSettingsController
{
    public Task<IActionResult> CreateNewUser(User user);
    public Task<IActionResult> DeleteUser(string name);
    public Task<IActionResult> UpdateUser(User user);
    public Task<IActionResult> GetLimitUser(int limit);
    public Task<IActionResult> GetNameUser(string name);
}

[Route("api/v1/[controller]")]
[ApiController]
public class AdminPanelSettingsController : ControllerBase, IAdminPanelSettingsController
{
    private IRepository<User> _repository;

    public AdminPanelSettingsController(IRepository<User> repository)
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

    [HttpGet("[action]{limit:int}")]
    public Task<IActionResult> GetLimitUser(int limit)
    {
        throw new NotImplementedException();
    }

    [HttpGet("[action]{name}")]
    public Task<IActionResult> GetNameUser(string name)
    {
        throw new NotImplementedException();
    }
}