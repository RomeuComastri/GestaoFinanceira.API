using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;

public interface IUsuarioAplicacao
{
    Task<int> VerificarLoginUsuario(string email, string senha);
    Task<int> SalvarUsuarioAsync(Usuario usuario);
    Task AtualizarUsuarioAsync(Usuario usuario);
    Task AlterarSenhaUsuarioAsync(Usuario usuario, string senhaAntiga);
    Task DesativarUsuarioAsync(int usuarioId);
    Task RestaurarUsuarioAsync(int usuarioId);
    Task<Usuario> ObterUsuarioPorIdAsync(int usuarioId);
    Task<IEnumerable<Usuario>> ListarUsuariosAsync();
}