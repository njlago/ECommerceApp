

using ECommerceAPI.Models;
using ECommerceAPI.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Infrastructure;

public class UserRepository : IUserRepository
{

    private AppDbContext appDbContext;

    public UserRepository(AppDbContext appDbContext) {
        this.appDbContext = appDbContext;
    }



    public User Login([FromBody] Login user)
    {
        // Find user by email
        User userFound = this.appDbContext.Users.FirstOrDefault(u => u.Email == user.Email);
        if (userFound == null)
            throw new NotFoundException("User not found.");

        // Check password
        if (userFound.PasswordHash != user.PasswordHash)
            throw new UnauthorizedException("Invalid credentials.");

        return userFound;
    }

    public bool Register([FromBody] User user) {

        User userFound = this.appDbContext.Users.Where(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash).FirstOrDefault();

        if (userFound != null) {
            return false;
        }

        appDbContext.Users.Add(user);
        appDbContext.SaveChanges();
        return true;

    }
}