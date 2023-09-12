using webapi_playground.Models.Domain.Auth;

namespace webapi_playground.Repository.Interface.AuthRepo;

public interface IJwtTokenGenerator
{
     Task<string> CreateTokenAsync(UserDomain userDomain);
}
