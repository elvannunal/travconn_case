// Domain/UserHobby.cs
namespace Hobbies.Domain.Entites;

public class UserHobby
{
    public int Id { get; set; }

    // Identity service'ten gelen user referansı
    public Guid UserId { get; set; } // sadece referans
    
    public int HobbyId { get; set; }

    public DateTime StartedDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }

    // Navigation - Hobby bizim DB'mizde
    public virtual Hobby Hobby { get; set; } = null!;
}