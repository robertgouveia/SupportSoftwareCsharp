using Microsoft.EntityFrameworkCore;
using SupportMVC.Models;

namespace SupportMVC.InMemory;

public class UserContextInMemory(DbContextOptions<UserContextInMemory> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}