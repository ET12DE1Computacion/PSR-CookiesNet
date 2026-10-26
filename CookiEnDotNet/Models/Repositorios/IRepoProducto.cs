namespace CookiEnDotNet.Models.Repositorios;
public interface IRepoProducto
{
    IEnumerable<Producto> Obtener();
    Producto? Obtener(int id);
}