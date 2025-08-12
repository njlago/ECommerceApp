using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
public class AppDbContext : DbContext
{
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


public DbSet<User> Users { get; set; }
public DbSet<Role> Roles { get; set; } 
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
base.OnModelCreating(modelBuilder);


modelBuilder.Entity<User>()
.HasIndex(u => u.Email)
.IsUnique();


modelBuilder.Entity<User>()
.Property(u => u.Email)
.IsRequired()
.HasMaxLength(200);


modelBuilder.Entity<User>()
.Property(u => u.FullName)
.IsRequired()
.HasMaxLength(120);


}
}
}
