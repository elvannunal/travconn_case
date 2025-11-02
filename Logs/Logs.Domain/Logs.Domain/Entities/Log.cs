namespace Logs.Domain.Entities;

public class Log
{
    public int Id { get; set; }
    public string Operation { get; set; } // 'Create', 'Update', 'TokenRequest'
    public string EntityType { get; set; } // 'Hobby', 'UserHobby', 'Token'
    public string EntityData { get; set; } // JSON 
    public string UserId { get; set; }
    public DateTime TimeStamp { get; set; }
}