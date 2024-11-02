using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SupportMVC.InMemory;
using SupportMVC.Models;
using SupportMVC.Services;

namespace SupportMVC.Controllers;

public class SignUpController(UserContextInMemory context) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string user, string pass)
    {
        var userInstance = UserService.Register(user, pass);
        await context.Users.AddAsync(userInstance);
        await context.SaveChangesAsync();

        SignIn(userInstance);
        
        return RedirectToAction("Index", "Home");
    }

    private async void SignIn(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        try
        {
            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SignInAsync error: {ex.Message}");
        }
    }
}