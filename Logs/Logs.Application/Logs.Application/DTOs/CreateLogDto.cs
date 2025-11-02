namespace Logs.Application.DTOs;

public class CreateLogDto
{
    public string Operation { get; set; }
    public string EntityType { get; set; }
    public object Data { get; set; }
    public string UserId { get; set; }
}