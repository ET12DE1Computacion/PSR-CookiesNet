public class Producto
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public decimal PrecioUnitario { get; set; }
}