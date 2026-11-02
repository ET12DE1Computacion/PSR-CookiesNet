namespace CookiEnDotNet.Models.Repositorios;

public interface IRepoCompra
{
    void Agregar(Compra compra);
    IEnumerable<Compra> ObtenerDelUsuario(int usuarioId);
    Compra? ObtenerPorNro(ulong nroCompra);
}