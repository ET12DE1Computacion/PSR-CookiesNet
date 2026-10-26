using CookiEnDotNet.Models.Repositorios;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Singleton para que la lista en memoria persista y sea la misma para todos los usuarios.
// (En una BD real USAMOS AddScoped)
builder.Services.AddSingleton<IRepoProducto, RepoProductooMem>();
builder.Services.AddSingleton<IRepoUsuario, RepoUsuarioMem>();

// Configuramos el esquema de Autenticación por Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Cuenta/Login"; 
        options.AccessDeniedPath = "/Home/Index"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// Activamos la seguridad en el pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();