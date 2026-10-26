namespace CookiEnDotNet.Models;

public class Usuario
{
    public int Id { get; set; }
    public required string NombreUsuario { get; set; }
    public required string Password { get; set; }
    public string Rol { get; set; } = "Cliente";
}
