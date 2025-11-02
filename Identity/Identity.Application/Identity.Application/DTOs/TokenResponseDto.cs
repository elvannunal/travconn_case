namespace Identity.Application.DTOs;

public class TokenResponseDto
{
    public required string Token { get; set; }
    public required Guid UserId { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}