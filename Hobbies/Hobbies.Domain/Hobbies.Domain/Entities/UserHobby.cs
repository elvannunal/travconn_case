using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hobbies.Domain.Common;
using Hobbies.Domain.Entites;

namespace Hobbies.Domain.Entities;

public class UserHobby: BaseEntity
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid BaseHobbyId { get; set; }

    public int HobbyId { get; set; }
    
    public DateTime StartedDate { get; set; } = DateTime.UtcNow;
    
    [MaxLength(1000)]
    public string? Notes { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    
    [ForeignKey("HobbyId")] 
    public virtual Hobby Hobby { get; set; } = null!;
}

