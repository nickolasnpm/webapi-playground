using System.ComponentModel.DataAnnotations;

namespace webapi_playground.Models.DTOs.Auth;

public class Login
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
