using Logs.Application.Interfaces;
using Logs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logs.Infrastructure.Data;

public class LogsDbContext: DbContext 
{
    public LogsDbContext(DbContextOptions<LogsDbContext> options) : base(options)
    {
    }

    public DbSet<Log> Logs { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Operation).IsRequired().HasMaxLength(50);
            entity.Property(e => e.EntityType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(450);
            entity.Property(e => e.TimeStamp).IsRequired();

            entity.HasIndex(e => e.EntityType);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.TimeStamp);
        });
    }
}