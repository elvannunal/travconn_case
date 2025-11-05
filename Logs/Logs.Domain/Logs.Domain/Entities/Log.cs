using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logs.Domain.Entities;

public class Log
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Operation { get; set; } // 'Create', 'Update', 'TokenRequest'

    [Required]
    [StringLength(100)]
    public string EntityType { get; set; } // 'Hobby', 'UserHobby', 'Token'

    public string EntityData { get; set; } // JSON 

    [Required]
    [StringLength(128)]
    public string UserId { get; set; }

    [StringLength(45)]
    public string IpAddress { get; set; }

    [Required]
    public DateTime TimeStamp { get; set; }
}