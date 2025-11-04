using System.ComponentModel.DataAnnotations;

namespace Hobbies.Domain.Common;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}