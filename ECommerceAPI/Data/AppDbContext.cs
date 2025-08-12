using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "admin@gmail.com", Password = "Passcode1", Role = Roles.Admin }
                );
        }
    }
}
