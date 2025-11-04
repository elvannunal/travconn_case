using Hobbies.Application.DTOs;
using Hobbies.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hobbies.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HobbiesController : ControllerBase
{
    private readonly IHobbyService _hobbyService;

    public HobbiesController(IHobbyService hobbyService)
    {
        _hobbyService = hobbyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var hobbies = await _hobbyService.GetAllHobbiesAsync();
            return Ok(hobbies);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var hobby = await _hobbyService.GetHobbyByIdAsync(id);
            
            if (hobby == null)
                return NotFound(new { message = $"Hobby with ID {id} not found." });

            return Ok(hobby);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHobbyDto createHobbyDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hobbyDto = await _hobbyService.CreateHobbyAsync(createHobbyDto);
            
            return CreatedAtAction(nameof(GetById), new { id = hobbyDto.Id }, hobbyDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHobby(Guid id, [FromBody] UpdateHobbyDto updateHobbyDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedHobby = await _hobbyService.UpdateHobbyAsync(id, updateHobbyDto);
            
            if (updatedHobby == null)
                return NotFound(new { message = $"Hobby with ID {id} not found." });

            return Ok(updatedHobby);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _hobbyService.DeleteHobbyAsync(id);

            if (!result)
                return NotFound(new { message = $"Hobby with ID {id} not found." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
}