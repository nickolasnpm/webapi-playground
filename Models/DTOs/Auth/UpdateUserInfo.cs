using System.ComponentModel.DataAnnotations;

namespace webapi_playground.Models.DTOs.Auth;

public class UpdateUserInfo
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}
