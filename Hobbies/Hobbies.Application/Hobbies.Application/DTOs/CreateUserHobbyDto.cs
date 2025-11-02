namespace Hobbies.Application.DTOs;

public class CreateUserHobbyDto
{
    public Guid UserId { get; set; }
    public int HobbyId { get; set; }
    public DateTime StartedDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}