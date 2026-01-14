using Microsoft.EntityFrameworkCore;
using notification.Domain.Entities;

namespace notification.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    DbSet<User> Users { get; set; }
    public AppDbContext(DbContextOptions options) : base (options)
    {
        
    }
}