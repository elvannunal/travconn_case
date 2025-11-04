namespace Hobbies.Application.DTOs;

public class UserHobbyDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid HobbyId { get; set; }
    public string HobbyName { get; set; } = string.Empty;
    public string? Notes { get; set; }
}