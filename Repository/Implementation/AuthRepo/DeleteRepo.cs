using webapi_playground.Data;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Repository.Implementation.AuthRepo;

public class DeleteRepo: IDeleteRepo
{
    private readonly SchoolMemberDbContext _dbContext;

    public DeleteRepo(SchoolMemberDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<UserRolesDomain?> DeleteRolesInfo(Guid id)
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
