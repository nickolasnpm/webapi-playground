namespace webapi_playground.Models.Domain.Auth;

public class RolesDomain
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<UserRolesDomain> UserRolesDomain { get; set; }
}
