using Hobbies.Domain.Entites;
using Hobbies.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hobbies.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<UserHobby> UserHobbies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Hobby>(entity =>
        {
            entity.HasIndex(e => e.Name);
        });

        //user hobby entity sınıfını konfigürasları
        modelBuilder.Entity<UserHobby>(entity =>
        {
            entity.HasOne(e => e.Hobby)
                .WithMany()
                .HasForeignKey(e => e.BaseHobbyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.BaseHobbyId);
        });
       
    }
}