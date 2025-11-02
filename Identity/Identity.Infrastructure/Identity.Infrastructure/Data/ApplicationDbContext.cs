using Identity.Application.Interfaces;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext :DbContext, IApplicationDbContext
{  
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    
    public DbSet<Account> Accounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var hasher = new PasswordHasher<Account>();
      
        var user1Id = Guid.Parse("f7be2cea-9450-47ce-b47c-2765db336eb1"); 
        var user2Id = Guid.Parse("dfa86cdf-de8a-469a-801d-eda786b908b6"); 
        var user3Id = Guid.Parse("c9336243-d5bd-46f3-8b8f-cc754afbc398");
       
        modelBuilder.Entity<Account>().HasData(
            new Account()
            {
                Id = user1Id,
                Email = "user@gmail.com",
                FirstName = "User1 LastName",
                LastName = "User1 Lastname",
                PasswordHash = hasher.HashPassword(null!, "sifreUser1Sifre1"),
                Role = "Admin"
            },
            new Account()
            {
                Id = user2Id,
                Email = "user2@gmail.com",
                FirstName = "User2 LastName",
                LastName = "User2 Lastname",
                PasswordHash = hasher.HashPassword(null!, "sifreUser2Sifre2"),
                Role = "User"
            },
            new Account()
            {
                Id = user3Id,
                Email = "user3@gmail.com",
                FirstName = "User3 LastName",
                LastName = "User3 Lastname",
                PasswordHash = hasher.HashPassword(null!, "sifreUser3Sifre3"),
                Role = "User"
            }
        );
    }

}