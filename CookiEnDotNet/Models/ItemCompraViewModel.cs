namespace CookiEnDotNet.Models;

public class ItemCompraVM
{
    public int ProductoId { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public decimal PrecioUnitario { get; set; }
    public byte Cantidad { get; set; } = 0;
}

public class CrearCompraVM
{
    public List<ItemCompraVM> Productos { get; set; } = new();
}