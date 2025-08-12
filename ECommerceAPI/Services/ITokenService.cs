using ECommerceAPI.Models;

public interface ITokenService
{
    string GenerateToken(User user);
}