using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Admin;

public interface IAdminSettingsController
{
    public Task<IActionResult> CreateNewUser();
}

public class AdminSettingsController : ControllerBase, IAdminSettingsController
{
    public Task<IActionResult> CreateNewUser()
    {
        throw new NotImplementedException();
    }
}