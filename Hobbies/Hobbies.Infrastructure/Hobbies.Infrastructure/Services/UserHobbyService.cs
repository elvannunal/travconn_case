using System.Security.Claims;
using Hobbies.Application.DTOs;
using Hobbies.Application.Interfaces;
using Hobbies.Domain.Entites;
using Hobbies.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace Hobbies.Infrastructure.Services;

public class UserHobbyService : IUserHobbyService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserHobbyService(
        ApplicationDbContext context, 
        ILogService logService,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _logService = logService;
        _httpContextAccessor = httpContextAccessor;
    }

    //  Token'dan User ID al
    private string GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
    }

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
                HobbyId = uh.HobbyId,
                HobbyName = uh.Hobby?.Name ?? string.Empty,
                Notes = uh.Notes
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"UserHobby listesi alınırken hata: {ex.Message}", ex);
        }
    }

    public async Task<UserHobbyDto?> GetUserHobbyByIdAsync(int id)
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
                HobbyId = userHobby.HobbyId,
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
            // UserHobby oluştur
            var userHobby = new UserHobby
            {
                UserId = createDto.UserId,
                HobbyId = createDto.HobbyId,
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
                await _logService.LogAsync(
                    "Create",          
                    "UserHobby",       
                    userHobby,         
                    GetCurrentUserId() 
                );
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"Log kaydedilemedi: {logEx.Message}");
            }

            return new UserHobbyDto
            {
                Id = userHobby.Id,
                UserId = userHobby.UserId,
                HobbyId = userHobby.HobbyId,
                HobbyName = hobby?.Name ?? string.Empty,
                Notes = userHobby.Notes
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"UserHobby oluşturulurken hata: {ex.Message}", ex);
        }
    }

    public async Task<UserHobbyDto?> UpdateUserHobbyAsync(int id, UpdateUserHobbyDto updateDto)
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
                await _logService.LogAsync(
                    "Update",          
                    "UserHobby",        
                    userHobby,         
                    GetCurrentUserId() 
                );
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"Log kaydedilemedi: {logEx.Message}");
            }

            return new UserHobbyDto
            {
                Id = userHobby.Id,
                UserId = userHobby.UserId,
                HobbyId = userHobby.HobbyId,
                HobbyName = userHobby.Hobby?.Name ?? string.Empty,
                Notes = userHobby.Notes
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"UserHobby güncellenirken hata: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteUserHobbyAsync(int id)
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