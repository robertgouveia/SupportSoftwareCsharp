using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportMVC.InMemory;
using SupportMVC.Models;

namespace SupportMVC.Controllers;

[Authorize]
public class ConnectionController(ConnectionContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var connections = await context.Connections.ToListAsync();
        return View(connections);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, string ip, string database)
    {
        var connection = new Connection
        {
            Name = name,
            IP = ip,
            Database = database
        };

        await context.Connections.AddAsync(connection);
        await context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var conn = await context.Connections.FindAsync(id);
        if (conn is null) return RedirectToAction("Index");

        return View(conn);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string name, string ip, string database)
    {
        var conn = await context.Connections.FindAsync(id);
        if (conn is null) return RedirectToAction("Index");

        conn.Name = name;
        conn.Database = database;
        conn.IP = ip;

        context.Connections.Update(conn);
        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var conn = await context.Connections.FindAsync(id);
        if (conn is null) return RedirectToAction("Index");

        context.Connections.Remove(conn);
        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}