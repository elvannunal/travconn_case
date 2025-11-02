namespace Hobbies.Application.DTOs;

public class UpdateUserHobbyDto
{
    public DateTime UpdatedDate { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
}