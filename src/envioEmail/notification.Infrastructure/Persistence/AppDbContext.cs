    using Microsoft.EntityFrameworkCore;
    using notification.Domain.Entities;

    namespace notification.Infrastructure.Persistence; 
    
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }
    }