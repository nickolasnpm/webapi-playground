using webapi_playground.Models.Domain.Auth;

namespace webapi_playground.Repository.Interface.AuthRepo;

public interface IRegisterRepo
{
    Task<UserDomain> RegisterUser(UserDomain userDomain);
    Task<RolesDomain> RegisterRole(RolesDomain roleDomain);
    Task<UserRolesDomain> RegisterUserRole(UserRolesDomain userRoleDomain);
}
