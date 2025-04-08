using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
[Route("[Controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly Authenticationservices _services;
    public AuthenticationController(Authenticationservices services)
    {
        _services = services;
    }

    [HttpPost("Login")]
    [AllowAnonymous]

    public async Task<IActionResult> Login([FromBody] LoginDTO info)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response { success = true, message = " Invalid login credentials" });
        var result = await _services.Authenticate(info);
        if (!result.success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}