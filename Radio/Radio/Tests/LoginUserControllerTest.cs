using NUnit.Framework;
using Radio.Controller.Authorization;

namespace Radio.Tests;

[TestFixture]
public class LoginUserControllerTest(ILoginUserController loginUserController)
{
    [SetUp]
    public void SetUp()
    {
        loginUserController = new LoginUserController();
    }

    [Test]
    public void LoginTest()
    {
        loginUserController.Login("", "");
        Assert.Pass();
    }
}