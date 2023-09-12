using Microsoft.EntityFrameworkCore;
using webapi_playground.Data;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Repository.Implementation.AuthRepo;

public class LoginRepo: ILoginRepo
{
    private readonly SchoolMemberDbContext _dbContext;

    public LoginRepo(SchoolMemberDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<UserDomain> AuthenticateAsync(string email)
    {
        // check the user existence in the system
        UserDomain? userDomain = await _dbContext.UserTable.FirstOrDefaultAsync(x =>
        EF.Functions.Collate(x.Email, "SQL_Latin1_General_CP1_CS_AS") == email);

        if (userDomain != null)
        {
            userDomain.Roles = new List<string>();

            foreach (var userRole in await _dbContext.UserRolesTable.Where(x => x.UserID == userDomain.Id).ToListAsync())
            {
                var role = await _dbContext.RolesTable.FirstOrDefaultAsync(x => x.Id == userRole.RoleID);

                if (role != null)
                {
                    userDomain.Roles.Add(role.Title);
                }
            }
        }

        return userDomain!;
    }
}
