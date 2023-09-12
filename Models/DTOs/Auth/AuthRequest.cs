using System.ComponentModel.DataAnnotations;

namespace webapi_playground.Models.DTOs.Auth;

public class AuthRequest
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public string Email { get; set; }
    public List<Roles> Roles { get; set; }
    
    [Required]
    public string Password { get; set; }
}
