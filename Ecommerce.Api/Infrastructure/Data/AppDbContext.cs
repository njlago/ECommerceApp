using Ecommerce.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Infrastructure.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>().HasIndex(u => u.Email).IsUnique();
        b.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(200);
        b.Entity<User>().Property(u => u.FullName).IsRequired().HasMaxLength(120);
    }
}
