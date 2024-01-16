using Microsoft.AspNetCore.Mvc;
using Radio.Controller.Admin;
using Radio.Model;

namespace RadioTest;

[TestFixture]
public class AdminSettingsPanelControllerTest
{
    private IAdminPanelSettingsController _adminPanelSettings;
    
    [SetUp]
    public void SetUp()
    {
        _adminPanelSettings = new AdminPanelSettingsController();
    }

    [Test]
    public async Task AdminSettingsPanelDeleteReturnStatusCodeOk()
    {
        var result = await _adminPanelSettings.DeleteUser("Vasia");
        Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task AdminSettingsPanelCreateReturnStatusCodeOk()
    {
        var result = await _adminPanelSettings.CreateNewUser(new User("", "", true, false, true, false));
        Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task AdminSettingsPanelUpdateReturnStatusCodeOk()
    {
        var result = await _adminPanelSettings.UpdateUser(new User("", "", true, false, true, false));
        Assert.That(result, Is.InstanceOf<OkResult>());
    }
}