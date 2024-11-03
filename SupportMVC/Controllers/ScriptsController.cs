using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportMVC.Helpers;
using SupportMVC.InMemory;
using SupportMVC.Models;

namespace SupportMVC.Controllers;

[Authorize]
public class ScriptsController(ScriptContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var scripts = await context.Scripts.ToListAsync();
        return View(scripts);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, IFormFile? content)
    {
        if (content is null || content.Length <= 0) return View();
        var extension = Path.GetExtension(content.FileName);
        if (extension != ".sql") return View();

        using var reader = new StreamReader(content.OpenReadStream());
        var data = await reader.ReadToEndAsync();

        var script = new Script
        {
            Name = name,
            Content = data,
            Variables = ScriptHelper.GetVariables(data)
        };

        await context.Scripts.AddAsync(script);
        await context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var script = await context.Scripts.FindAsync(id);
        if (script is null) return RedirectToAction("Index");
        return View(script);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string name, string content)
    {
        var script = await context.Scripts.FindAsync(id);
        if (script is null) return RedirectToAction("Edit");

        script.Content = content;
        script.Name = name;
        script.Variables = ScriptHelper.GetVariables(content);

        context.Scripts.Update(script);
        await context.SaveChangesAsync();

        return View(script);
    }
}