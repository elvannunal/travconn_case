using Hobbies.Application.DTOs;

namespace Hobbies.Application.Interfaces;

public interface IHobbyService
{
    Task<IEnumerable<HobbyDto>> GetAllHobbiesAsync();
    Task<HobbyDto?> GetHobbyByIdAsync(Guid id);
    Task<HobbyDto> CreateHobbyAsync(CreateHobbyDto createDto);
    Task<HobbyDto?> UpdateHobbyAsync(Guid id, UpdateHobbyDto updateDto);
    Task<bool> DeleteHobbyAsync(Guid id);
}