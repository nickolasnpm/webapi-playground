using webapi_playground.Data;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Repository.Implementation.AuthRepo;

public class UpdateRepo : IUpdateRepo
{
    private readonly SchoolMemberDbContext _dbContext;

    public UpdateRepo(SchoolMemberDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDomain?> UpdateUserInfo(Guid id, UserDomain userDomain)
    {

        UserDomain? existing = await _dbContext.UserTable.FindAsync(id);

        if (existing == null)
        {
            return null;
        }
        else
        {
            existing.FullName = userDomain.FullName;
            existing.Email = userDomain.Email;
            existing.PasswordHash = userDomain.PasswordHash;
            existing.PasswordSalt = userDomain.PasswordSalt;
            await _dbContext.SaveChangesAsync();
        }

        return existing;
    }

    public async Task<UserRolesDomain?> UpdateRolesInfo (Guid id, UserRolesDomain userRolesDomain)
    {
        UserRolesDomain? existing = await _dbContext.UserRolesTable.FindAsync(id);

        if (existing == null)
        {
            return null;
        }
        else
        {
            existing.UserID = userRolesDomain.UserID;
            existing.RoleID = userRolesDomain.RoleID;
            await _dbContext.SaveChangesAsync();
        }

        return existing;
    }

    public async Task<UserRolesDomain?> DeleteRolesInfo (Guid id)
    {
        UserRolesDomain? existing = await _dbContext.UserRolesTable.FindAsync(id);

        if (existing != null)
        {
            _dbContext.UserRolesTable.Remove(existing);
            await _dbContext.SaveChangesAsync();
        }

        return existing;
    }
    
}
