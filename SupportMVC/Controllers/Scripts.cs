using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportMVC.Views.Scripts;

namespace SupportMVC.Controllers;

public class Scripts : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        var scripts = new List<Script>();
        return View(scripts);
    }
}