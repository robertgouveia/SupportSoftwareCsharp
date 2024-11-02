using SupportBusiness.Authentication;
using SupportMVC.Models;

namespace SupportMVC.Services;

public class UserService()
{
    public static User Register(string username, string password)
    {
        var user = new User
        {
            Username = username
        };
        
        PasswordHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        return user;
    }
}