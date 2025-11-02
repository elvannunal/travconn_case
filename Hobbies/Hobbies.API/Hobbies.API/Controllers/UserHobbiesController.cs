using Hobbies.Application.DTOs;
using Hobbies.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hobbies.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserHobbiesController : ControllerBase
{
    private readonly IUserHobbyService _userHobbyService;

    public UserHobbiesController(IUserHobbyService userHobbyService)
    {
        _userHobbyService = userHobbyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserHobby()
    {
        try
        {
            var userHobbies = await _userHobbyService.GetAllUserHobbiesAsync();
            return Ok(userHobbies);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdUserHobby(int id)
    {
        try
        {
            var userHobby = await _userHobbyService.GetUserHobbyByIdAsync(id);
            
            if (userHobby == null)
                return NotFound(new { message = $"UserHobby with ID {id} not found." });

            return Ok(userHobby);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserHobby([FromBody] CreateUserHobbyDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userHobbyDto = await _userHobbyService.CreateUserHobbyAsync(createDto);
            
            return CreatedAtAction(nameof(GetByIdUserHobby), new { id = userHobbyDto.Id }, userHobbyDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserHobby(int id, [FromBody] UpdateUserHobbyDto updateUserHobbyDto)
    {
        try
        {
            // Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userHobbyDto = await _userHobbyService.UpdateUserHobbyAsync(id, updateUserHobbyDto);
            
            if (userHobbyDto == null)
                return NotFound(new { message = $"UserHobby with ID {id} not found." });

            return Ok(userHobbyDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserHobby(int id)
    {
        try
        {
            var result = await _userHobbyService.DeleteUserHobbyAsync(id);
            
            if (!result)
                return NotFound(new { message = $"UserHobby with ID {id} not found." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
}