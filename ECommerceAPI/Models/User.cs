using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage ="Full Name is null and must have a value.")]
    public string FullName { get; set; }
    [Required(ErrorMessage ="Email field is null and must have a value.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "PasswordHash is null and must have a value.")]
    public string PasswordHash { get; set; }
    [Required(ErrorMessage ="Role must be either Customer or Admin.")]
    public string Role { get; set; }
}
