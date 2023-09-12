using webapi_playground.Models.Domain.Auth;

namespace webapi_playground.Repository.Interface.AuthRepo;

public interface IUpdateRepo
{
    Task<UserDomain?> UpdateUserInfo(Guid id, UserDomain userDomain);
    Task<UserRolesDomain?> UpdateRolesInfo(Guid id, UserRolesDomain userRolesDomain);
}
