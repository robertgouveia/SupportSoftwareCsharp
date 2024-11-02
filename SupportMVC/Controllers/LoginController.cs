using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SupportBusiness.Authentication;
using SupportMVC.InMemory;
using SupportMVC.Services;

namespace SupportMVC.Controllers;

public class LoginController(UserContextInMemory context) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string user, string pass)
    {
        var userInstance = context.Users.FirstOrDefault(u => u.Username == user);
        if (userInstance is null) return RedirectToAction("Index", "Login");

        if (!PasswordHelper.VerifyPasswordHash(pass, userInstance.PasswordHash, userInstance.PasswordSalt))
            return RedirectToAction("Index", "Login");
        
        UserService.Login(userInstance, out var claimsIdentity, out var authProperties);
        await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

        return RedirectToAction("Index", "Home");
    }
}