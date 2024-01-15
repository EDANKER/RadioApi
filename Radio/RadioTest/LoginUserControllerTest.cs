using Radio.Controller.Authorization;

namespace RadioTest;

public class LoginUserControllerTest
{
    private LoginUserController _loginUserController;
    
    [SetUp]
    public void Setup()
    {
        _loginUserController = new LoginUserController();
    }

    [Test]
    public async Task Test1()
    {
        if (_loginUserController.Login("", "").Status == (TaskStatus)200)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }
}