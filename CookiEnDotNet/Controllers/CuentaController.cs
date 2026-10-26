using CookiEnDotNet.Models;
using CookiEnDotNet.Models.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookiEnDotNet.Controllers;
public class CuentaController : Controller
{
    private readonly IRepoUsuario _repo;
    public CuentaController(IRepoUsuario repo) => _repo = repo;
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loguinVM)
    {
        if (!ModelState.IsValid) return View(loguinVM);

        var usuario = _repo.Loguear(loguinVM.NombreUsuario, loguinVM.Password);

        if (usuario != null)
        {
            //Armamos las declaraciones (Claims) con los datos del usuario obtenido
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //Creamos la cookie para la sesión
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction(nameof(MiCuenta));
        }

        ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
        return View(loguinVM);
    }
    // PÚBLICO: Cerrar sesión
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    // PRIVADO - Solo usuarios con Cookie válida
    [Authorize]
    public IActionResult MiCuenta()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userIdClaim, out int id))
        {
            // Consultamos la información completa desde el repositorio
            var usuario = _repo.Obtener(id);
            if (usuario != null)
                return View(usuario);
        }

        return RedirectToAction("Logout");
    }
    
    [HttpGet]
    public IActionResult Registro()
        => View(new Usuario { NombreUsuario = "", Password = "" });

    [HttpPost]
    public IActionResult Registro(Usuario usuario)
    {
        if (!ModelState.IsValid)
            return View(usuario);

        //Aca la capa de servicio deberia de encriptar la pass
        //ó lo pueden hacer en la bd
        _repo.Registrar(usuario);

        // Redirigimos al Login para que inicie sesión con sus datos
        return RedirectToAction("Login");
    }
}