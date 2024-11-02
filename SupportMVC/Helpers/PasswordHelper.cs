using System.Security.Cryptography;
using System.Text;

namespace SupportBusiness.Authentication;

public static class PasswordHelper
{
    public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using var hmac = new HMACSHA256(); // Generates a new SHA256 key
        passwordSalt = Convert.ToBase64String(hmac.Key);
        passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        // HMAC Compute Hash takes bytes and encodes it into the HMAC.
    }

    public static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
    {
        using var hmac = new HMACSHA256(Convert.FromBase64String(storedSalt));
        // HMAC key matching the salt
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        // Encoded entry
        return Convert.ToBase64String(computedHash) == storedHash;
        // Comparison
    }
}