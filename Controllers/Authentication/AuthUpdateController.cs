using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Models.DTOs.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Controllers.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthUpdateController : ControllerBase
{
    private readonly IUpdateRepo _updateRepo;
    private readonly IDeleteRepo _deleteRepo;
    private readonly IReadRepo _readRepo;
    private readonly IPasswordEncryption _passwordEncryption;
    private readonly IRegisterRepo _registerRepo;

    public AuthUpdateController(IDeleteRepo deleteRepo, IUpdateRepo updateRepo, IReadRepo readRepo, IPasswordEncryption passwordEncryption, IRegisterRepo registerRepo)
    {
        _deleteRepo = deleteRepo;
        _updateRepo = updateRepo;
        _readRepo = readRepo;
        _passwordEncryption = passwordEncryption;
        _registerRepo = registerRepo;
    }

    /// <summary>
    /// Update User Info Async
    /// </summary>
    /// <remarks>
    /// Example Request:
    /// 
    ///     POST /register
    ///     { 
    ///         "fullname": "ronaldo",
    ///         "email": "ronaldo@gmail.com",
    ///         "password": "1234"
    ///     }
    ///     
    /// Example Response:
    /// 
    ///     ```json
    ///     { 
    ///         "fullname": "ronaldo",
    ///         "email": "ronaldo@gmail.com",
    ///         "password": "1234"
    ///     }
    /// </remarks>
    /// <param name="userId"></param>
    /// <param name="update"></param>
    /// <returns></returns>
    [HttpPut("update-user-info")]
    [Authorize]
    public async Task<IActionResult> UpdateUserInfoAsync(Guid userId, UpdateUserInfo update)
    {
        if (Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!) != userId)
        {
            return BadRequest("You are not allowed to access this information");
        }

        UserDomain? userDomain = new();

        if (await _readRepo.GetUserByEmail(update.Email) != null)
        {
            return BadRequest("Email Address Already Exists");
        }
        else
        {
            _passwordEncryption.CreatePasswordHash(update.Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            userDomain!.FullName = update.FullName;
            userDomain.Email = update.Email;
            userDomain.PasswordHash = PasswordHash;
            userDomain.PasswordSalt = PasswordSalt;

            await _updateRepo.UpdateUserInfo(userId, userDomain);
        }

        return Ok(update);
    }

    /// <summary>
    /// Update Roles Info Async
    /// </summary>
    /// <remarks>
    /// Example Request:
    /// 
    ///     POST /register
    ///     { 
    ///        "roles": [
    ///             {
    ///                 "title": "string"
    ///             },
    ///             {
    ///                 "title": "string"
    ///             }
    ///         ]
    ///     }
    ///     
    /// Example Response:
    /// 
    ///     ```json
    ///     { 
    ///        "roles": [
    ///             {
    ///                 "title": "string"
    ///             },
    ///             {
    ///                 "title": "string"
    ///             }
    ///         ]
    ///     }
    /// </remarks>
    /// <param name="userId"></param>
    /// <param name="update"></param>
    /// <returns></returns>
    [HttpPut("update-roles-info")]
    [Authorize(Roles = "principal")]
    public async Task<IActionResult> UpdateRolesInfoAsync(Guid userId, UpdateRolesInfo update)
    {
        // check the availability of the user that owns the userId
        AuthResponse? response = await _readRepo.GetUserById(userId);

        if (response == null)
        {
            return NotFound();
        }

        RolesDomain? rolesDomain = new RolesDomain();
        UserRolesDomain? userRolesDomain = new UserRolesDomain();

        List<Guid> updatedRoleID = new();
        response.Roles = new();

        // check the availability of the role, if no, create
        foreach (var input in update.Roles.Select(role => role.Title).ToList())
        {
            var roleInDB = await _readRepo.GetRolesByTitle(input);

            if (roleInDB != null)
            {
                rolesDomain.Id = roleInDB!.Id;
            }
            else
            {
                rolesDomain.Title = input;
                rolesDomain = await _registerRepo.RegisterRole(rolesDomain);
            }
            // add role Id to updatedRoleID list for later use
            updatedRoleID.Add(rolesDomain.Id);
            response.Roles.Add(input);
        }

        // get the id of the existing role belong to user
        foreach (var userRole in await _readRepo.GetUserRoleByUserID(userId))
        {
            // get the title of the existing role belong to user
            foreach (var role in await _readRepo.GetRolesViaUserRoleDomain(userRole.RoleID))
            {
                // if the existing role is similar to the input role, if none, delete to make space for new role
                if (updatedRoleID.Contains(role.Id))
                {
                    return BadRequest($"The user has already hold {role.Title.ToUpper()} role");
                }
                else
                {
                    await _deleteRepo.DeleteRolesInfo(userRole.Id);
                }
            }
        };

        // using the role id collected earlier, create new role
        foreach (var roleId in updatedRoleID)
        {
            userRolesDomain.UserID = userId;
            userRolesDomain.RoleID = roleId;
            await _registerRepo.RegisterUserRole(userRolesDomain);
        }
        
        return Ok(response);
    }   
}