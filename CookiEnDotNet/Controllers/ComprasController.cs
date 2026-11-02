using CookiEnDotNet.Models;
using CookiEnDotNet.Models.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookiEnDotNet.Controllers;

[Authorize]
public class ComprasController : Controller
{
    private readonly IRepoProducto _repoProducto;
    private readonly IRepoUsuario _repoUsuario;
    private readonly IRepoCompra _repoCompra;

    public ComprasController(IRepoProducto repoProducto, IRepoUsuario repoUsuario, IRepoCompra repoCompra)
    {
        _repoProducto = repoProducto;
        _repoUsuario = repoUsuario;
        _repoCompra = repoCompra;
    }
    private IActionResult RedirigirLogin
        => RedirectToAction("Login", "Cuenta");

    [HttpGet]
    public IActionResult Crear()
    {
        var productos = _repoProducto.Obtener();

        var model = new CrearCompraVM
        {
            Productos = productos.Select(p => new ItemCompraVM
            {
                ProductoId = p.Id,
                NombreProducto = p.Nombre,
                PrecioUnitario = p.PrecioUnitario,
                Cantidad = 0
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Crear(CrearCompraVM model)
    {
        // Filtramos solo los productos que el usuario sumo
        var seleccionados = model.Productos.Where(p => p.Cantidad > 0).ToList();

        if (seleccionados.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Debe ingresar una cantidad mayor a 0 en al menos un producto.");
            return View(model);
        }

        // Obtenemos el ID del usuario desde los Claims de la Cookie
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out int idUsuario))
            return RedirigirLogin;

        var cliente = _repoUsuario.Obtener(idUsuario);
        if (cliente == null)
            return NotFound("Usuario no encontrado.");

        var detalles = new List<Detalle>();
        foreach (var item in seleccionados)
        {
            var producto = _repoProducto.Obtener(item.ProductoId);
            if (producto != null)
                detalles.Add(new Detalle(producto, item.Cantidad));
        }

        var nuevaCompra = new Compra(cliente, detalles);

        _repoCompra.Agregar(nuevaCompra);

        return View("Exito", nuevaCompra);
    }

    [HttpGet]
    public IActionResult Historial()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out int userId))
            return RedirigirLogin;

        var compras = _repoCompra.ObtenerDelUsuario(userId);
        return View(compras.OrderByDescending(c => c.FechaHora));
    }

    [HttpGet]
    public IActionResult Detalle(ulong id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out int userId))
            return RedirigirLogin;

        var compra = _repoCompra.ObtenerPorNro(id);

        // validaciones de existencia y seguridad
        if (compra == null)
            return NotFound("La compra no existe.");

        if (compra.Cliente.Id != userId)
            // 403 Forbidden si intenta ver la compra de otra persona
            return Forbid();

        return View(compra);
    }
}