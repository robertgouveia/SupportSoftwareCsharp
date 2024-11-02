using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportMVC.Views.Scripts;

namespace SupportMVC.Controllers;

[Authorize]
public class ScriptsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Create()
    {
        return View();
    }
}