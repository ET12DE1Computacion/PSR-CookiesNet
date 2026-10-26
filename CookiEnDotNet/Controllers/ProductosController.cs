using CookiEnDotNet.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace CookiEnDotNet.Controllers;
public class ProductosController : Controller
{
    private readonly IRepoProducto _repo;

    public ProductosController(IRepoProducto repo) => _repo = repo;

    public IActionResult Index()
    {
        var productos = _repo.Obtener();
        return View(productos);
    }

    public IActionResult Detalle(int id)
    {
        var producto = _repo.Obtener(id);
        if (producto is null)
            return NotFound();
        
        return View(producto);
    }
}