using System.Diagnostics.CodeAnalysis;

namespace CookiEnDotNet.Models;
public class Detalle
{
    public required byte Cantidad { get; set; }
    public required decimal PrecioUnitario { get; set; }
    public required Producto Producto { get; init; }
    public string NombreProducto => Producto.Nombre;
    [SetsRequiredMembers]
    public Detalle(Producto producto, byte cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
        PrecioUnitario = producto.PrecioUnitario;
    }
}