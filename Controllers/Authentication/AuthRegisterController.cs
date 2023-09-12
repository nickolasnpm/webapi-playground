using Microsoft.AspNetCore.Mvc;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Models.DTOs.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Controllers.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthRegisterController : ControllerBase
{
    private readonly IRegisterRepo _registerRepo;
    private readonly IReadRepo _readRepo;
    private readonly IPasswordEncryption _passwordEncryption;

    public AuthRegisterController(IReadRepo readRepo, IPasswordEncryption passwordEncryption, IRegisterRepo registerRepo)
    {
        _readRepo = readRepo;
        _passwordEncryption = passwordEncryption;
        _registerRepo = registerRepo;
    }

    /// <summary>
    /// Registr Async
    /// </summary>
    /// <remarks>
    /// Example Request:
    /// 
    ///     POST /register
    ///     { 
    ///         "fullname": "ronaldo",
    ///         "email": "ronaldo@gmail.com",
    ///         "roles": [ 
    ///             {
    ///                 "title": "user"
    ///             }
    ///         ],
    ///         "password": "1234"
    ///     }
    ///     
    /// Example Response:
    /// 
    ///     ```json
    ///     { 
    ///         "fullname": "ronaldo",
    ///         "email": "ronaldo@gmail.com",
    ///         "roles": [ 
    ///             {
    ///                 "title": "user"
    ///             }
    ///         ],
    ///         "password": "1234"
    ///     }
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(AuthRequest request)
    {

        #region Create userDomain property

        UserDomain? userDomain = new();

        if (await _readRepo.GetUserByEmail(request.Email) != null)
        {
            return BadRequest("Email Address Already Exists");
        }
        else
        {
            _passwordEncryption.CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            userDomain.FullName = request.FullName;
            userDomain.Email = request.Email;
            userDomain.PasswordHash = PasswordHash;
            userDomain.PasswordSalt = PasswordSalt;

            userDomain = await _registerRepo.RegisterUser(userDomain);
        }

        #endregion

        #region Create RolesDomain & UsersRolesDomain properties

        RolesDomain? rolesDomain = new RolesDomain();
        UserRolesDomain? userRolesDomain = new UserRolesDomain();

        foreach (var input in request.Roles.Select(role => role.Title).ToList())
        {
            var roleInDB = await _readRepo.GetRolesByTitle(input);

            if (roleInDB != null)
            {
                rolesDomain.Id = roleInDB.Id;
            }
            else
            {
                rolesDomain.Title = input;
                rolesDomain = await _registerRepo.RegisterRole(rolesDomain);
            }
            userRolesDomain.UserID = userDomain.Id;
            userRolesDomain.RoleID = rolesDomain.Id;
            userRolesDomain = await _registerRepo.RegisterUserRole(userRolesDomain);
        }

        #endregion

        AuthResponse response = new()
        {
            Id = userDomain.Id,
            FullName = request.FullName,
            Email = request.Email,
            Roles = request.Roles.Select(role => role.Title).ToList()
        };

        return Ok(response);
    }
}
