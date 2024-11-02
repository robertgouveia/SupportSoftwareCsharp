using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SupportMVC.InMemory;
using SupportMVC.Models;

namespace SupportMVC.Controllers;

public class HomeController(UserContextInMemory context) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}