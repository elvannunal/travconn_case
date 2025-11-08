using System.Security.Claims;
using Hobbies.Application.DTOs;
using Hobbies.Application.Interfaces;
using Hobbies.Domain.Entites;
using Hobbies.Domain.Entities;
using Hobbies.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hobbies.Infrastructure.Services;

public class UserHobbyService : IUserHobbyService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogApiClient _logApiClient;
    private readonly ILogger<UserHobbyService> _logger;
    private readonly ICurrentUserService _currentUserService;

    public UserHobbyService(
        ApplicationDbContext context, 
        IHttpContextAccessor httpContextAccessor, 
        ILogApiClient logApiClient,
        ILogger<UserHobbyService> logger, ICurrentUserService currentUserService)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _logApiClient = logApiClient;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    //  Token'dan User ID al
 /*   private string GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
    }*/

    public async Task<IEnumerable<UserHobbyDto>> GetAllUserHobbiesAsync()
    {
        try
        {
            var userHobbies = await _context.UserHobbies
                .Include(uh => uh.Hobby)
                .AsNoTracking()
                .ToListAsync();

            return userHobbies.Select(uh => new UserHobbyDto
            {
                Id = uh.Id,
                UserId = uh.UserId,
                HobbyId = uh.BaseHobbyId,
                HobbyName = uh.Hobby?.Name ?? string.Empty,
                Notes = uh.Notes
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"UserHobby listesi alınırken hata: {ex.Message}", ex);
        }
    }

    public async Task<UserHobbyDto?> GetUserHobbyByIdAsync(Guid id)
    {
        try
        {
            var userHobby = await _context.UserHobbies
                .Include(uh => uh.Hobby)
                .AsNoTracking()
                .FirstOrDefaultAsync(uh => uh.Id == id);

            if (userHobby == null) return null;

            return new UserHobbyDto
            {
                Id = userHobby.Id,
                UserId = userHobby.UserId,
                HobbyId = userHobby.BaseHobbyId,
                HobbyName = userHobby.Hobby?.Name ?? string.Empty,
                Notes = userHobby.Notes
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"UserHobby detayı alınırken hata: {ex.Message}", ex);
        }
    }

  public async Task<UserHobbyDto> CreateUserHobbyAsync(CreateUserHobbyDto createDto)
{
    try
    {
       
        var hobbyExists = await _context.Hobbies
            .AsNoTracking()
            .AnyAsync(h => h.Id == createDto.HobbyId);

        if (!hobbyExists)
        {
            throw new Exception($"Hata: Belirtilen Hobi ID'si ({createDto.HobbyId}) bulunamadı.");
        }

        var userHobby = new UserHobby
        {
            UserId = createDto.UserId,
            BaseHobbyId = createDto.HobbyId,
            Notes = createDto.Notes,
            StartedDate = createDto.StartedDate
        };

        _context.UserHobbies.Add(userHobby);
        await _context.SaveChangesAsync();

        var hobby = await _context.Hobbies
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.Id == createDto.HobbyId);

        try
        {
            await _logApiClient.SendLogAsync(new CreateLogDto
            {
                Operation = "CREATE",
                EntityType = "UserHobby",
                Data = createDto,
                UserId = _currentUserService.UserId
            });
        }
        catch (Exception logEx)
        {
            _logger.LogWarning(logEx, 
                "Harici Log API'ye (CREATE UserHobby) kayıt gönderilemedi. Hata: {ErrorMessage}", // 🚨 Log mesajı düzeltildi
                logEx.Message);
        }

        return new UserHobbyDto
        {
            Id = userHobby.Id,
            UserId = userHobby.UserId,
            HobbyId = userHobby.BaseHobbyId,
            HobbyName = hobby?.Name ?? string.Empty,
            Notes = userHobby.Notes
        };
    }
    catch (Exception ex) // Yakalanan hata değişkeni 'ex' olarak düzeltildi.
    {
        // DB hataları veya FK hataları burada fırlatılır.
        throw new Exception($"UserHobby oluşturulurken hata: {ex.Message}", ex);
    }
}

    public async Task<UserHobbyDto?> UpdateUserHobbyAsync(Guid id, UpdateUserHobbyDto updateDto)
    {
        try
        {
            var userHobby = await _context.UserHobbies
                .Include(uh => uh.Hobby)
                .FirstOrDefaultAsync(uh => uh.Id == id);

            if (userHobby == null) return null;

            userHobby.Notes = updateDto.Notes;
            userHobby.UpdatedDate = updateDto.UpdatedDate;

            await _context.SaveChangesAsync();

            try
            {
                await _logApiClient.SendLogAsync(new CreateLogDto
                {
                    Operation = "Update",
                    EntityType = "Hobby",
                    Data = updateDto,
                    UserId = _currentUserService.UserId
                });
            }
            catch (Exception logEx)
            {
                _logger.LogWarning(logEx, 
                    "Harici Log API'ye (CREATE Hobby) kayıt gönderilemedi. Hata: {ErrorMessage}", 
                    logEx.Message);
            }
            return new UserHobbyDto
            {
                Id = userHobby.Id,
                UserId = userHobby.UserId,
                HobbyId = userHobby.BaseHobbyId,
                HobbyName = userHobby.Hobby?.Name ?? string.Empty,
                Notes = userHobby.Notes
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"UserHobby güncellenirken hata: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteUserHobbyAsync(Guid id)
    {
        try
        {
            var userHobby = await _context.UserHobbies.FindAsync(id);
            if (userHobby == null) return false;

            _context.UserHobbies.Remove(userHobby);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"UserHobby silinirken hata: {ex.Message}", ex);
        }
    }
}