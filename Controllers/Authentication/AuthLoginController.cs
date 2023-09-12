using Microsoft.AspNetCore.Mvc;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Models.DTOs.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Controllers.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthLoginController : ControllerBase
{
    private readonly ILoginRepo _loginRepo;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordEncryption _passwordEncryption;

    public AuthLoginController(ILoginRepo loginRepo, IJwtTokenGenerator jwtTokenGenerator, IPasswordEncryption passwordEncryption)
    {
        _loginRepo = loginRepo;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordEncryption = passwordEncryption;
    }

    /// <summary>
    /// Login Async
    /// </summary>
    /// <remarks>
    /// Example:
    /// Example Request:
    /// 
    ///     POST /update
    ///     { 
    ///         "email": "ronaldo@gmail.com",
    ///         "password": "1234"
    ///     }
    ///     
    /// Example Response:
    /// 
    ///     ```json
    ///     { 
    ///         "email": "ronaldo@gmail.com",
    ///         "password": "1234"
    ///     }
    /// </remarks>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(Login login)
    {
        // check the existence of inserted username
        UserDomain? authenticatedUser = await _loginRepo.AuthenticateAsync(login.Email);

        if (authenticatedUser != null)
        {
            // verify the password hash & password salt
            if (!_passwordEncryption.VerifyPasswordHash(login.Password, authenticatedUser.PasswordHash, authenticatedUser.PasswordSalt))
            {
                return BadRequest("Wrong Password. Please try again");
            }

            // create token for user authorization
            var token = await _jwtTokenGenerator.CreateTokenAsync(authenticatedUser);

            return Ok($"Authorization Token : {token}");
        }
        else
        {
            return BadRequest("Email does not exist!");
        }
    }
}
