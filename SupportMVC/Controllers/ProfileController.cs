using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportMVC.InMemory;
using SupportMVC.Models;

namespace SupportMVC.Controllers;

[Authorize]
public class ProfileController(UserContextInMemory context) : Controller
{
    public async Task<IActionResult> Index()
    {
        if (User.Identity is null) return RedirectToAction("Index", "Home");
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == User.Identity.Name);
        if (user is null) return RedirectToAction("Index", "Home");
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Index(string user, string pass)
    {
        if (!ModelState.IsValid || User.Identity is null) return RedirectToAction("Index");
        var userInstance = await context.Users.FirstOrDefaultAsync(u => u.Username == User.Identity.Name);
        if (userInstance is null) return RedirectToAction("Index");

        userInstance.SQLUsername = user;
        userInstance.SQLPassword = pass;
        context.Users.Update(userInstance);

        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}