using Identity.Application.DTOs;

namespace Identity.Application.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto registerDto);

    Task<TokenResponseDto?> LoginAsync(LoginDto loginDto);
    
    Task<IEnumerable<UserListDto>> GetAllUsersAsync();
}