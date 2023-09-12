using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Repository.Implementation.AuthRepo;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> CreateTokenAsync(UserDomain userDomain)
    {
        // System.Security.Claims.Claim implement claims-based identity in .NET;
        // Claims represent attributes of the subject (user) that are useful in the context of authentication and authorization operations. 

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.GivenName, userDomain.FullName),
            new Claim(ClaimTypes.Email, userDomain.Email),
            new Claim(ClaimTypes.NameIdentifier, userDomain.Id.ToString()),
        };

        userDomain.Roles.ForEach((role) =>
        {
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()!));
        });

        // Encoding the valus of JWT:KEY saved in the appsetting.json
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        // credentials represent the digital signature produced via signingcredentials class
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // create token for authorization
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
