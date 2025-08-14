using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
public class AppDbContext : DbContext
{
public AppDbContext() { }
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


public DbSet<User> Users { get; set; }
public virtual DbSet<Product> Products { get; set; }
public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "admin", Email = "admin@gmail.com", PasswordHash = "Passcode1", Role = Roles.Admin }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food" },
                new Category { Id = 2, Name = "Apparel" },
                new Category { Id = 3, Name = "Electronics"}
                );
        }
}
}
