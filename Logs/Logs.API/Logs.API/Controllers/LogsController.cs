using Logs.Application.DTOs;
using Logs.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logs.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly ILogService _logService;
    private readonly ILogger<LogsController> _logger;

    public LogsController(ILogService logService, ILogger<LogsController> logger)
    {
        _logService = logService;
        _logger = logger;
    }

 
    // Identity ve Hobbies API'lerinden log kayıtları alır,Token ve Hobbies parametreleri ile log kaydeder
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddLog([FromBody] CreateLogDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _logService.AddLogAsync(createDto);
            return Ok(new { Message = "Log kaydedildi" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Log kaydedilemedi");
            return BadRequest(new { Message = "Log kaydedilemedi", Error = ex.Message });
        }
    }

    /// Tarih aralığı ve log tipi ile filtrelenmiş logları getirir ve Token ve Hobbies parametreleri ile log kaydeder
 
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetLogs([FromQuery] LogFilterDto filter)
    {
        try
        {
            var logs = await _logService.GetLogsAsync(filter);
            return Ok(logs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Loglar alınamadı");
            return BadRequest(new { Message = "Loglar alınamadı", Error = ex.Message });
        }
    }

    /// Belirli bir kullanıcının tüm loglarını getirir
    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetUserLogs(string userId)
    {
        try
        {
            var filter = new LogFilterDto { UserId = userId };
            var logs = await _logService.GetLogsAsync(filter);
            return Ok(logs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Kullanıcı logları alınamadı: {UserId}", userId);
            return BadRequest(new { Message = "Kullanıcı logları alınamadı", Error = ex.Message });
        }
    }
}