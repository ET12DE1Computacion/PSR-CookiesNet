namespace CookiEnDotNet.Models.Repositorios;

public interface IRepoUsuario
{

    Usuario? Loguear(string username, string password);
    Usuario? Obtener(int id);
    void Registrar(Usuario usuario);
}