namespace Hobbies.Application.DTOs;

public class UserHobbyDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int HobbyId { get; set; }
    public string HobbyName { get; set; } = string.Empty;
    public string? Notes { get; set; }
}