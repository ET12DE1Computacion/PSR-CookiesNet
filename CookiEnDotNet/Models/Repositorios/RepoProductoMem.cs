namespace CookiEnDotNet.Models.Repositorios;
public class RepoProductooMem : IRepoProducto
{
    private readonly List<Producto> _productos;

    public RepoProductooMem()
        => _productos =
            [
                new Producto { Id = 1, Nombre = "Laptop Gamer", Descripcion = "16GB RAM, RTX 4060", PrecioUnitario = 1200.50m },
                new Producto { Id = 2, Nombre = "Mouse Inalámbrico", Descripcion = "Ergonómico, batería recargable", PrecioUnitario = 25.00m },
                new Producto { Id = 3, Nombre = "Teclado Mecánico", Descripcion = "Switches red, RGB", PrecioUnitario = 85.99m }
            ];
    public IEnumerable<Producto> Obtener() => _productos;

    public Producto? Obtener(int id) => _productos.FirstOrDefault(p => p.Id == id);
}
