using Microsoft.EntityFrameworkCore;

namespace SupportMVC.Services;

public static class InMemoryContextService
{
    public static void AddInMemoryContext<T>(this IServiceCollection services, string dbName) where T : DbContext
    {
        services.AddDbContext<T>(options =>
        {
            options.UseInMemoryDatabase(dbName);
        });
    }
}