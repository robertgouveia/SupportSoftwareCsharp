using Microsoft.EntityFrameworkCore;
using SupportMVC.InMemory;
using SupportMVC.Models;
using SupportMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInMemoryContext<UserContext>("Users");
builder.Services.AddInMemoryContext<ConnectionContext>("Connections");
builder.Services.AddInMemoryContext<ScriptContext>("Scripts");

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.LoginPath = "/SignUp";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        config.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
