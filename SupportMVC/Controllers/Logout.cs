using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SupportMVC.Controllers;

public class LogoutController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Index()
    {
        await HttpContext.SignOutAsync("CookieAuth");

        return RedirectToAction("Index", "SignUp");
    }
}