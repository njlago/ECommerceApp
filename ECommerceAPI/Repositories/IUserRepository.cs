using ECommerceAPI.Models;

public interface IUserRepository
{
    User Login(Login user);

    bool Register(User user);


}