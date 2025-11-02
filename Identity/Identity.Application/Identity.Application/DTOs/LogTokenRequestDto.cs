namespace Identity.Application.DTOs;

public class LogTokenRequestDto
{
    public Guid UserId { get; set; }
    
    public string LogType { get; set; } = "Token";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string Email { get; set; } = string.Empty;
    
    public string Role { get; set; } = string.Empty;
    
    public DateTime TokenExpiration { get; set; }
    
    public string Message { get; set; } = string.Empty;
}