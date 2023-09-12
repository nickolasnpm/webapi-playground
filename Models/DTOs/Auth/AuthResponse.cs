namespace webapi_playground.Models.DTOs.Auth;

public class AuthResponse
{
    public Guid Id {get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}
