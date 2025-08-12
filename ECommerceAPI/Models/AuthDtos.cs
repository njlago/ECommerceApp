namespace ECommerceAPI.Models
{
    public record RegisterRequest(string FullName, string Email, string Password);
public record LoginRequest(string Email, string Password);
public record LoginResponse(string Token, string Role, string FullName, string Email);
}