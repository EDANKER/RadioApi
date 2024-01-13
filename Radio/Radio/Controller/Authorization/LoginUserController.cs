using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.Authorization;

public interface ILoginUserController
{
    
}
[Route("api/v1/[controller]")]
[ApiController]
public class LoginUserController : ControllerBase, ILoginUserController
{
    
}