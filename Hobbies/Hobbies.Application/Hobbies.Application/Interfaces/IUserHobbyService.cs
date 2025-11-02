using Hobbies.Application.DTOs;

namespace Hobbies.Application.Interfaces;

public interface IUserHobbyService
{
    Task<IEnumerable<UserHobbyDto>> GetAllUserHobbiesAsync();
    Task<UserHobbyDto?> GetUserHobbyByIdAsync(int id);
    Task<UserHobbyDto> CreateUserHobbyAsync(CreateUserHobbyDto createDto);
    Task<UserHobbyDto?> UpdateUserHobbyAsync(int id, UpdateUserHobbyDto updateDto);
    Task<bool> DeleteUserHobbyAsync(int id);
}