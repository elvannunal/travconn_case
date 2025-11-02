namespace Hobbies.Application.DTOs;

public class LogContext
{
    public string? Token { get; set; }
    public Guid? UserId { get; set; }
    public string? Username { get; set; }
    public string? Endpoint { get; set; }
    public string? HttpMethod { get; set; }
    public string? IpAddress { get; set; }
    public string? CorrelationId { get; set; }
}