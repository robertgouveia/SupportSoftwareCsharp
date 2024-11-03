using Microsoft.EntityFrameworkCore;
using SupportMVC.Models;

namespace SupportMVC.InMemory;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}