using EMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AuthController : Controller
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public IActionResult GetJwt() => Json(_authService.GetJwt());
}
