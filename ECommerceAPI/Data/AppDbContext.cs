using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
public class AppDbContext : DbContext
{
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


public DbSet<User> Users { get; set; }
public DbSet<Product> Products { get; set; }
protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "admin", Email = "admin@gmail.com", PasswordHash = "Passcode1", Role = Roles.Admin }
                );
        }
}
}
