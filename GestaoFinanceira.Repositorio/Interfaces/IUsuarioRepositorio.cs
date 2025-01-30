using GestaoFinanceira.Dominio.Entidades;

public interface IUsuarioRepositorio
{
    Task<int> SalvarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task<Usuario> ObterPorIdAsync(int id);
    Task<Usuario> ObterPorEmailAsync(string email);
    Task<IEnumerable<Usuario>> ListarAsync();
}