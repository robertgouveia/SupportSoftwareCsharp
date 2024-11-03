using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SupportMVC.InMemory;
using SupportMVC.Services;

namespace SupportMVC.Controllers;

public class SignUpController(UserContext context) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string user, string pass, string passConfirm)
    {
        if (pass != passConfirm) return RedirectToAction("Index", "SignUp");
        
        var userInstance = UserService.Register(user, pass);
        await context.Users.AddAsync(userInstance);
        await context.SaveChangesAsync();
        
        UserService.Login(userInstance, out var claimsIdentity, out var authProperties);
        await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);
        
        return RedirectToAction("Index", "Home");
    }
}