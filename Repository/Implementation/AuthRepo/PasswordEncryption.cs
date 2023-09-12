using System.Security.Cryptography;
using System.Text;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Repository.Implementation.AuthRepo;

public class PasswordEncryption : IPasswordEncryption
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        passwordSalt = hmac.Key;
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}
