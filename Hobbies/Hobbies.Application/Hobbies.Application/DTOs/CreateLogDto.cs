namespace Hobbies.Application.DTOs;

public class CreateLogDto
{
    public string Operation { get; set; } = string.Empty; 

    public string EntityType { get; set; } = string.Empty; 

    public string UserId { get; set; } = string.Empty;

    public object Data { get; set; } = new object(); 
    
}