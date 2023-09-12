namespace webapi_playground.Models.Domain.Auth;

public class UserRolesDomain
{
    public Guid Id { get; set; }
    public Guid UserID { get; set; }
    public UserDomain UserDomain { get; set; }
    public Guid RoleID { get; set; }
    public RolesDomain RoleDomain { get; set; }
}
