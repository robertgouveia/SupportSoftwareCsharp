using Microsoft.EntityFrameworkCore;
using SupportMVC.Models;

namespace SupportMVC.InMemory;

public class ConnectionContext(DbContextOptions<ConnectionContext> options) : DbContext(options)
{
    public DbSet<Connection> Connections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Connection>().HasData(
            new Connection{ Id = 1, Name = "Pizza Express", Database = "TRGDB01", IP = "10.10.10.10" },
            new Connection{ Id = 2, Name = "Pizza Hut", Database = "TRGDB02", IP = "10.10.10.10" }
        );
    }
}