using System.Security.Claims;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto);

        if (result)
        {
            return Ok(new { Message = "Registration successful" });
        }
        
        return BadRequest(new { Message = "Registration failed. User may already exist." }); 
    }

    [HttpPost("login")]
    [AllowAnonymous] 
    [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var tokenResponse = await _authService.LoginAsync(loginDto);

        if (tokenResponse == null)
        {
            return Unauthorized(new { Message = "Invalid credentials or user not found." });
        }

        return Ok(tokenResponse);
    }
    [HttpGet("users")]
    [Authorize(Roles = "Admin")] // Sadece Admin rolüne sahip kullanıcılar erişebilir
    [ProducesResponseType(typeof(IEnumerable<UserListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _authService.GetAllUsersAsync();

        if (users == null)
        {
            return Ok(Enumerable.Empty<UserListDto>());
        }
        
        return Ok(users);
    }
    [HttpGet("protected-test")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult ProtectedTest()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userRole = User.FindFirstValue(ClaimTypes.Role);
        
        return Ok(new 
        { 
            Message = "You have access to the protected endpoint.",
            UserId = userId,
            Role = userRole
        });
    }

    [HttpGet("admin-test")]
    [Authorize(Roles = "Admin")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult AdminTest()
    {
        return Ok(new { Message = "You have Admin access to the protected endpoint." });
    }
}