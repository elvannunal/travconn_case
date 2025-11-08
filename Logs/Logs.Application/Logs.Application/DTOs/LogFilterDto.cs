namespace Logs.Application.DTOs;

public class LogFilterDto
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Log Tipi (Operation)
    public string? Operation { get; set; }

    // EntityType (Token/Hobbies)
    //  "Token ve Hobbies parametreleri" ne karşılık olarak entitytype
    public string? EntityType { get; set; } 
    
    public string? UserId { get; set; }
    public string IpAddress { get; set; }

}