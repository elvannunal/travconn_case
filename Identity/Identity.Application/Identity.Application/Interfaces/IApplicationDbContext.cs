using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}