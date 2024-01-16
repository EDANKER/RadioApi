﻿using Microsoft.AspNetCore.Mvc;
using Radio.Model;

namespace Radio.Controller.Admin;

public interface IAdminPanelSettingsController
{
    public Task<IActionResult> CreateNewUser(User user);
    public Task<IActionResult> DeleteUser(int id);
    public Task<IActionResult> UpdateUser(User user);
}

[Route("api/v1/[controller]")]
[ApiController]
public class AdminPanelSettingsController : ControllerBase, IAdminPanelSettingsController
{

    [HttpPost("createNewUser")]
    public async Task<IActionResult> CreateNewUser(User user)
    {
        return Ok();
    }

    [HttpDelete("deleteUser")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        return Ok();
    }

    [HttpPut("updateUser")]
    public async Task<IActionResult> UpdateUser(User user)
    {
        return Ok();
    }
}