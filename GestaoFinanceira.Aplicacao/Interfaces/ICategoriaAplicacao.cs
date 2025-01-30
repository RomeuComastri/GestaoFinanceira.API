using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;

public interface ICategoriaAplicacao
{
    Task<int> SalvarCategoriaAsync(Categoria categoria);
    Task AtualizarCategoriaAsync(Categoria categoria);
    Task DesativarCategoriaAsync(int categoriaId);
    Task RestaurarCategoriaAsync(int categoriaId);
    Task<Categoria> ObterCategoriaPorIdAsync(int id);
    Task<IEnumerable<Categoria>> ListarCategoriasAsync(int usauarioId, bool ativo);

}