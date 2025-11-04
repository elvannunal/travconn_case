using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Entities;

public class Account
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string? FirstName { get; set; }
    
    [MaxLength(50)]
    public string? LastName { get; set; }
    
    [Required]
    [MaxLength(150)]
    public required string Email { get; set; }
    
    [Required]
    [MaxLength(256)]
    public required string PasswordHash { get; set; }
    
    [Required]
    [MaxLength(40)]
    public required string Role { get; set; }
}
