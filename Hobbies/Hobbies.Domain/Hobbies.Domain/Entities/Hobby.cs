using System.ComponentModel.DataAnnotations;
using Hobbies.Domain.Common;

namespace Hobbies.Domain.Entites;

public class Hobby :BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedDate { get; set; }
    
}

