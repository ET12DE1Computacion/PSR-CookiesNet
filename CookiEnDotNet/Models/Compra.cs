using System.Diagnostics.CodeAnalysis;

namespace CookiEnDotNet.Models;

public class Compra
{
    public ulong NroCompra { get; set; }
    public required DateTime FechaHora { get; set; }
    public required Usuario Cliente { get; set; }
    public required IEnumerable<Detalle> Detalles { get; set; }
    [SetsRequiredMembers]
    public Compra(Usuario cliente, IEnumerable<Detalle> detalles)
    {
        Cliente = cliente;
        Detalles = detalles;
        FechaHora = DateTime.Now;
    }
    public decimal TotalCompra
        => Detalles.Sum(d => d.PrecioUnitario);
}
