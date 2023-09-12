using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_playground.Models.Domain.Auth;
public class UserDomain
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    [NotMapped]
    public List<string> Roles { get; set; } 
    public List<UserRolesDomain> UserRolesDomain { get; set; }
}
