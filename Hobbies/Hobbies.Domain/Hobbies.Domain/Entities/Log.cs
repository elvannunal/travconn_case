namespace Hobbies.Domain.Entites;

public class Log {
    public int Id  { get; set; }
    public string Operation { get; set; }
    public string EntityType { get; set; }
    public string EntityData { get; set; }
    public string UserId { get; set; }
    public DateTime TimeStamp { get; set; }
}
