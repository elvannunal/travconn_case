using System.Security.Claims;
using Hobbies.Application.DTOs;
using Hobbies.Application.Interfaces;
using Hobbies.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Hobbies.Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Hobbies.Infrastructure.Services;

public class HobbyService : IHobbyService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogApiClient _logApiClient;
    private readonly ILogger<HobbyService> _logger;

    public HobbyService(
        ApplicationDbContext applicationDb,
        IHttpContextAccessor httpContextAccessor,
        ILogApiClient logApiClient,
        ILogger<HobbyService> logger
        )
    {
        _context = applicationDb;
        _httpContextAccessor = httpContextAccessor;
        _logApiClient = logApiClient;
        _logger = logger;
    }

    //  Token'dan User ID'yi al
    private string GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
    }
    public async Task<IEnumerable<HobbyDto>> GetAllHobbiesAsync()
    {
        try
        {
            var hobbies = await _context.Hobbies
                .AsNoTracking()
                .ToListAsync();

            return hobbies.Select(hobby => new HobbyDto
            {
                Id = hobby.Id,
                Description = hobby.Description,
                Name = hobby.Name,
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Hobi listesi alınırken hata: {ex.Message}", ex);
        }
    }

    public async Task<HobbyDto?> GetHobbyByIdAsync(Guid id)
    {
        try
        {
            var hobby = await _context.Hobbies.FindAsync(id);

            if (hobby == null) return null;

            return new HobbyDto
            {
                Id = hobby.Id,
                Description = hobby.Description,
                Name = hobby.Name
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Hobi detayı alınırken hata: {ex.Message}", ex);
        }
    }

    public async Task<HobbyDto> CreateHobbyAsync(CreateHobbyDto createDto)
    {
        try
        {
           
            var hobby = new Hobby
            {
                Name = createDto.Name,
                Description = createDto.Description
            };
            
            _context.Hobbies.Add(hobby);
            await _context.SaveChangesAsync();
            try
            {
                await _logApiClient.SendLogAsync(new CreateLogDto
                {
                    Operation = "CREATE",
                    EntityType = "Hobby",
                    Data = createDto,
                    UserId = GetCurrentUserId()
                });
            }
            catch (Exception logEx)
            {
                _logger.LogWarning(logEx, 
                    "Harici Log API'ye (CREATE Hobby) kayıt gönderilemedi. Hata: {ErrorMessage}", 
                    logEx.Message);
            }
        
            
            return new HobbyDto
            {
                Id = hobby.Id,
                Name = hobby.Name,
                Description = hobby.Description
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Hobi oluşturulurken hata: {ex.Message}", ex);
        }
    }

    public async Task<HobbyDto?> UpdateHobbyAsync(Guid id, UpdateHobbyDto updateDto)
    {
        try
        {
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null) return null;

            // Güncelle
            hobby.Name = updateDto.Name;
            hobby.Description = updateDto.Description;

            await _context.SaveChangesAsync();
            try
            {
                await _logApiClient.SendLogAsync(new CreateLogDto
                {
                    Operation = "UPDATE",
                    EntityType = "Hobby",
                    Data = updateDto,
                    UserId = GetCurrentUserId()
                });
            }
            catch (Exception logEx)
            {
                _logger.LogWarning(logEx, 
                    "Harici Log API'ye (CREATE Hobby) kayıt gönderilemedi. Hata: {ErrorMessage}", 
                    logEx.Message);
            }

            return new HobbyDto
            {
                Id = hobby.Id,
                Name = hobby.Name,
                Description = hobby.Description
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Hobi güncellenirken hata: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteHobbyAsync(Guid id)
    {
        try
        {
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null) return false;
            
            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Hobi silinirken hata: {ex.Message}", ex);
        }
    }
}