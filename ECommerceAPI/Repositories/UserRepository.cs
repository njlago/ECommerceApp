
using ECommerceAPI.Models;
using ECommerceAPI.Data;

public class UserRepository
{

    private AppDbContext appDbContext;

    public UserRepository(AppDbContext appDbContext) {
        this.appDbContext = appDbContext;
    }


    public User Login(Login user)
    {
        User userFound = this.appDbContext.Users.Where(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash).FirstOrDefault();

        return userFound;
    }

    public bool Register(User user) {

        User userFound = this.appDbContext.Users.Where(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash).FirstOrDefault();

        if (userFound != null) {
            return false;
        }

        appDbContext.Users.Add(user);
        appDbContext.SaveChanges();
        return true;

    }
}