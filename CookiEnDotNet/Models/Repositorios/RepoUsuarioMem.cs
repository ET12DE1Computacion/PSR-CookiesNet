namespace CookiEnDotNet.Models.Repositorios;

public class RepoUsuarioMem : IRepoUsuario
{
    private readonly List<Usuario> _usuarios;
    public RepoUsuarioMem() => _usuarios =
    [
        new Usuario { Id = 1, NombreUsuario = "alumno", Password = "1234", Rol = "Estudiante" },
        new Usuario { Id = 2, NombreUsuario = "profe", Password = "1234", Rol = "Administrador" }
    ];

    public Usuario? Loguear(string username, string password)
        => _usuarios.FirstOrDefault(
                u => u.NombreUsuario.Equals(username) &&
                    u.Password == password);

    public Usuario? Obtener(int id)
        => _usuarios.FirstOrDefault(u => u.Id == id);

    public void Registrar(Usuario usuario)
    {
        usuario.Id = _usuarios.Any() ? _usuarios.Max(u => u.Id) + 1 : 1;
        _usuarios.Add(usuario);
    }
}