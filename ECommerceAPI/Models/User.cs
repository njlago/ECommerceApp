namespace EcommerceAPI.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = Roles.Customer;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
