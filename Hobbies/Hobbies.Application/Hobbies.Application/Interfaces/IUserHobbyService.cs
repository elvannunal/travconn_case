using Hobbies.Application.DTOs;

namespace Hobbies.Application.Interfaces;

public interface IUserHobbyService
{
    Task<IEnumerable<UserHobbyDto>> GetAllUserHobbiesAsync();
    Task<UserHobbyDto?> GetUserHobbyByIdAsync(Guid id);
    Task<UserHobbyDto> CreateUserHobbyAsync(CreateUserHobbyDto createDto);
    Task<UserHobbyDto?> UpdateUserHobbyAsync(Guid id, UpdateUserHobbyDto updateDto);
    Task<bool> DeleteUserHobbyAsync(Guid id);
}