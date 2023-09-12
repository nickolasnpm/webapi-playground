using Microsoft.EntityFrameworkCore;
using webapi_playground.Data;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Models.DTOs.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Repository.Implementation.AuthRepo;

public class ReadRepo : IReadRepo
{
    private readonly SchoolMemberDbContext _dbContext;

    public ReadRepo(SchoolMemberDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AuthResponse>> GetUserList()
    {
        List<AuthResponse> responseList = new();

        foreach (var user in _dbContext.UserTable.ToList())
        {
            AuthResponse response = new()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Roles = new List<string>()
            };

            GetRoleTitle(user.Id, response);

            responseList.Add(response);
        };

        await Task.CompletedTask;
        return responseList;
    }


    public async Task<AuthResponse?> GetUserById(Guid id)
    {
        UserDomain? userDomain = await _dbContext.UserTable.FirstOrDefaultAsync(x => x.Id == id);

        List<AuthResponse> responseList = new();
        AuthResponse response = new()
        {
            Id = userDomain!.Id,
            FullName = userDomain.FullName,
            Email = userDomain.Email,
            Roles = new List<string>()
        };

        GetRoleTitle(userDomain.Id, response);

        responseList.Add(response);

        await Task.CompletedTask;
        return response;
    }

    private void GetRoleTitle(Guid id, AuthResponse response)
    {
        foreach (var userRole in _dbContext.UserRolesTable.Where(userRole => userRole.UserID == id).ToList())
        {
            var role = _dbContext.RolesTable.FirstOrDefault(x => x.Id == userRole.RoleID);

            if (role != null)
            {
                response.Roles.Add(role.Title);
            }
        };
    }

    public async Task<UserDomain?> GetUserByEmail(string email) 
    {
        return await _dbContext.UserTable.FirstOrDefaultAsync(x => x.Email == email); 
    }

    public async Task<IEnumerable<RolesDomain>> GetRolesList()
    {
        return await _dbContext.RolesTable.ToListAsync();
    }

    public async Task<RolesDomain?> GetRolesById(Guid id)
    {
        return await _dbContext.RolesTable.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RolesDomain?> GetRolesByTitle(string title)
    {
        return await _dbContext.RolesTable.FirstOrDefaultAsync(x => x.Title == title);
    }

    public async Task<IEnumerable<RolesDomain>> GetRolesViaUserRoleDomain (Guid roleId)
    {
        var rolesDomain = _dbContext.RolesTable.Where(x => x.Id == roleId).ToList();

        await Task.CompletedTask;
        return rolesDomain;
    }

    public async Task<IEnumerable<UserRolesDomain>> GetUserRoleList()
    {
        return await _dbContext.UserRolesTable.ToListAsync();
    }

    public async Task<UserRolesDomain?> GetUserRoleById(Guid id)
    {
        return await _dbContext.UserRolesTable.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<UserRolesDomain>> GetUserRoleByUserID(Guid userId)
    {
        var userRolesDomain = _dbContext.UserRolesTable.Where(x => x.UserID == userId).ToList();

        await Task.CompletedTask;
        return userRolesDomain;
    }

    public async Task<IEnumerable<UserRolesDomain>> GetUserRoleByRoleID(Guid roleId)
    {
        var userRolesDomain = _dbContext.UserRolesTable.Where(x => x.RoleID == roleId).ToList();

        await Task.CompletedTask;
        return userRolesDomain;
    }
}
