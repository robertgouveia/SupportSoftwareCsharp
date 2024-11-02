using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
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

    public static void Login(User user, out ClaimsIdentity claimsIdentity, out AuthenticationProperties authProperties)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username)
        };

        claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
        authProperties = new AuthenticationProperties { IsPersistent = true };
    }
}