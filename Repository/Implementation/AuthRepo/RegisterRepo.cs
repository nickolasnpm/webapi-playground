using webapi_playground.Data;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Repository.Implementation.AuthRepo;

public class RegisterRepo : IRegisterRepo
{
    private readonly SchoolMemberDbContext _dbContext;

    public RegisterRepo(SchoolMemberDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDomain> RegisterUser(UserDomain userDomain)
    {
        userDomain.Id = Guid.NewGuid();
        await _dbContext.UserTable.AddAsync(userDomain);
        await _dbContext.SaveChangesAsync();
        return userDomain;
    }

    public async Task<RolesDomain> RegisterRole(RolesDomain roleDomain)
    {
        roleDomain.Id = Guid.NewGuid();
        await _dbContext.RolesTable.AddAsync(roleDomain);
        await _dbContext.SaveChangesAsync();
        return roleDomain;
    }

    public async Task<UserRolesDomain> RegisterUserRole(UserRolesDomain userRoleDomain)
    {
        userRoleDomain.Id = Guid.NewGuid();
        await _dbContext.UserRolesTable.AddAsync(userRoleDomain);
        await _dbContext.SaveChangesAsync();
        return userRoleDomain;
    }
}
