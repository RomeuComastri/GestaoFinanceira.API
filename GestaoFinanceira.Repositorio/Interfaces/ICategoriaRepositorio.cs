using GestaoFinanceira.Dominio.Entidades;

public interface ICategoriaRepositorio
{
    Task<int> SalvarAsync(Categoria categoria);
    Task AtualizarAsync(Categoria categoria);
    Task<Categoria> ObterPorIdAsync(int id);
    Task<IEnumerable<Categoria>> ListarAsync(int usuarioId, bool ativo);
}