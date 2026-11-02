namespace CookiEnDotNet.Models.Repositorios;

public class RepoCompraEnMemoria : IRepoCompra
{
    private static readonly List<Compra> _compras = [];
    private static ulong _siguienteNro = 1;

    public void Agregar(Compra compra)
    {
        compra.NroCompra = _siguienteNro++;
        _compras.Add(compra);
    }

    public IEnumerable<Compra> ObtenerDelUsuario(int usuarioId)
        => _compras.Where(c => c.Cliente.Id == usuarioId);

    public Compra? ObtenerPorNro(ulong nroCompra)
        => _compras.FirstOrDefault(c => c.NroCompra == nroCompra);

}
