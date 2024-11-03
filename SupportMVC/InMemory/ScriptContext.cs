using Microsoft.EntityFrameworkCore;
using SupportMVC.Models;

namespace SupportMVC.InMemory;

public class ScriptContext(DbContextOptions<ScriptContext> options) : DbContext(options)
{
    public DbSet<Script> Scripts { get; set; }
}