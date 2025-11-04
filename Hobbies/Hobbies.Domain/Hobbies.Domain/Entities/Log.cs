using System.ComponentModel.DataAnnotations;
using Hobbies.Domain.Common;

namespace Hobbies.Domain.Entites;

public class Log : BaseEntity
{
   
    [Required]
    [MaxLength(50)]
    public string Operation { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string EntityType { get; set; }
    
    [Required]
    [MaxLength(4000)]
    public string EntityData { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string UserId { get; set; }
    
    [Required]
    public DateTime TimeStamp { get; set; }
}
