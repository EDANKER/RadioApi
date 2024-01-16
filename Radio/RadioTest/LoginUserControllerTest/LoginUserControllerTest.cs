using Microsoft.AspNetCore.Mvc;
using Radio.Controller.Authorization;

namespace RadioTest;

[TestFixture]
public class LoginUserControllerTest
{
    private ILoginUserController _loginUserController;
    
    [SetUp]
    public void Setup()
    {
        _loginUserController = new LoginUserController();
    }

    [Test]
    public async Task LoginUserReturnStatusCodeOk()
    { 
        var result = await _loginUserController.Login("", "");
       Assert.That(result, Is.InstanceOf<OkResult>());
    }
}