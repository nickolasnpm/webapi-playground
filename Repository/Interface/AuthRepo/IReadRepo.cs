using webapi_playground.Models.Domain.Auth;
using webapi_playground.Models.DTOs.Auth;

namespace webapi_playground.Repository.Interface.AuthRepo;

public interface IReadRepo
{
     Task<List<AuthResponse>> GetUserList();
    Task<AuthResponse?> GetUserById(Guid id);
    Task<UserDomain?> GetUserByEmail(string email);
    Task<IEnumerable<RolesDomain>> GetRolesList();
    Task<RolesDomain?> GetRolesById(Guid id);
    Task<RolesDomain?> GetRolesByTitle(string title);
    Task<IEnumerable<RolesDomain>> GetRolesViaUserRoleDomain(Guid roleId);
    Task<IEnumerable<UserRolesDomain>> GetUserRoleList();
    Task<UserRolesDomain?> GetUserRoleById(Guid id);
    Task<IEnumerable<UserRolesDomain>> GetUserRoleByUserID(Guid userId);
    Task<IEnumerable<UserRolesDomain>> GetUserRoleByRoleID(Guid roleId);
}
