using Microsoft.AspNetCore.Mvc;
using Radio.Data.Repository.User;
using Radio.Model.RequestModel.User;
using Radio.Services.AdminPanelServices;


namespace Radio.Controller.AdminPanel.AdminPanelSettings;

public interface IAdminPanelSettingsController
{
    public Task<IActionResult> CreateNewUser(User user);
    public Task<IActionResult> DeleteUserId(int id);
    public Task<IActionResult> UpdateUser(User user, int id);
    public Task<IActionResult> GetUser(int limit);
    public Task<IActionResult> GetIdUser(int id);
}
[Route("api/v1/[controller]")]
[ApiController]
public class AdminPanelSettingsController : ControllerBase, IAdminPanelSettingsController
{
    private IUserRepository _userRepository;
    private IUserServices _userServices;

    public AdminPanelSettingsController(IUserRepository userRepository, IUserServices userServices)
    {
        _userRepository = userRepository;
        _userServices = userServices;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateNewUser(User user)
    {
        if (await _userRepository.Search("Users",user.FullName, user.Login))
        {
            return BadRequest("Такие данные уже есть");
        }
        
        return Ok(await _userRepository.CreateOrSave("Users", user));
    }

    [HttpDelete("[action]/{id:int}")]
    public async Task<IActionResult> DeleteUserId(int id)
    {
        return Ok(await _userRepository.DeleteId("Users", id));
    }

    [HttpPut("[action]/{id:int}")]
    public async Task<IActionResult> UpdateUser(User user, int id)
    {
        return Ok(await _userRepository.Update("Users", user, id));
    }

    [HttpGet("GetLimitUser/{limit:int}")]
    public async Task<IActionResult> GetUser(int limit)
    {
        return Ok(await _userServices.GetLimitUser("Users", limit));
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> GetIdUser(int id)
    {
        return Ok(await _userServices.GetIdUser("Users", id));
    }
}