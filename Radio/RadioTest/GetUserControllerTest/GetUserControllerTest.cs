using Microsoft.AspNetCore.Mvc;
using Radio.Controller.Admin.GetAllUser;

namespace RadioTest.GetUserControllerTest;

[TestFixture]
public class GetUserControllerTest
{
    private IGetUserController _getUserController;
    
    [SetUp]
    public void SetUp()
    {
        _getUserController = new GetUserController();
    }

    [Test]
    public async Task GetLimitUserReturnStatusCodeOk()
    {
       var result = await _getUserController.GetLimitUser(5);
       Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task GetNameUserReturnStatusCodeOk()
    {
        var result = await _getUserController.GetNameUser("Vasia");
        Assert.That(result, Is.InstanceOf<OkResult>());
    }
}