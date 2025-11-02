using Hobbies.Domain.Entites;
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
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Hobby Entity Configuration
        modelBuilder.Entity<Hobby>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);
            
            entity.HasIndex(e => e.Name);
        });

        // UserHobby Entity Configuration
        modelBuilder.Entity<UserHobby>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserId)
                .IsRequired(); 

            entity.Property(e => e.HobbyId)
                .IsRequired(); 

            entity.Property(e => e.Notes)
                .HasMaxLength(1000);
            
            entity.HasOne(e => e.Hobby)
                .WithMany()
                .HasForeignKey(e => e.HobbyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.HobbyId);
        });

        // Log Entity Configuration 
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Operation)
                .IsRequired()
                .HasMaxLength(50); 

            entity.Property(e => e.EntityType)
                .IsRequired()
                .HasMaxLength(100); 

            entity.Property(e => e.EntityData)
                .IsRequired()
                .HasMaxLength(4000); 

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.TimeStamp)
                .IsRequired();

            // Index'ler - Performans için
            entity.HasIndex(e => e.TimeStamp);
            entity.HasIndex(e => e.Operation);
            entity.HasIndex(e => e.EntityType);
            entity.HasIndex(e => e.UserId);
        });
    }
}