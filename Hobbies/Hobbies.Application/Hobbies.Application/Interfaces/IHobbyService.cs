using Hobbies.Application.DTOs;

namespace Hobbies.Application.Interfaces;

public interface IHobbyService
{
    Task<IEnumerable<HobbyDto>> GetAllHobbiesAsync();
    Task<HobbyDto?> GetHobbyByIdAsync(int id);
    Task<HobbyDto> CreateHobbyAsync(CreateHobbyDto createDto);
    Task<HobbyDto?> UpdateHobbyAsync(int id, UpdateHobbyDto updateDto);
    Task<bool> DeleteHobbyAsync(int id);
}