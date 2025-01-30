using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;
using GestaoFinanceira.Repositorio;

namespace GestaoFinanceira.Aplicacao
{
    public class CategoriaAplicacao : ICategoriaAplicacao
    {
        readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaAplicacao(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        public async Task<int> SalvarCategoriaAsync(Categoria categoria)
        {
            if (categoria == null)
                throw new Exception("A categoria não pode ser vazia");

            if (string.IsNullOrEmpty(categoria.Nome))
                throw new Exception("O nome não pode ser vazio");

            if (!Enum.IsDefined(typeof(TipoTransacao), categoria.Tipo))
                throw new Exception("O tipo de transação definida é inválido. Deve ser Despesa ou Receita");

            return await _categoriaRepositorio.SalvarAsync(categoria);
        }

        public async Task AtualizarCategoriaAsync(Categoria categoria)
        {
            var categoriaDominio = await _categoriaRepositorio.ObterPorIdAsync(categoria.Id);

            if (categoriaDominio == null)
                throw new Exception("Categoria não encontrada");

            categoriaDominio.Nome = string.IsNullOrEmpty(categoria.Nome) ? categoriaDominio.Nome : categoria.Nome;

            await _categoriaRepositorio.AtualizarAsync(categoriaDominio);
        }

        public async Task DesativarCategoriaAsync(int categoriaId)
        {
            var categoriaDominio = await _categoriaRepositorio.ObterPorIdAsync(categoriaId);

            if (categoriaDominio == null)
                throw new Exception("Categoria não encontrada no sistema");

            if (!categoriaDominio.Status)
                throw new Exception("Categoria ja desativada no sistema");

            categoriaDominio.Deletar();

            await _categoriaRepositorio.AtualizarAsync(categoriaDominio);
        }

        public async Task RestaurarCategoriaAsync(int categoriaId)
        {
            var categoriaDominio = await _categoriaRepositorio.ObterPorIdAsync(categoriaId);

            if (categoriaDominio == null)
                throw new Exception("Categoria não encontrada no sistema");

            if (categoriaDominio.Status)
                throw new Exception("Categoria ja ativa no sistema");

            categoriaDominio.Restaurar();

            await _categoriaRepositorio.AtualizarAsync(categoriaDominio);
        }

        public async Task<Categoria> ObterCategoriaPorIdAsync(int id)
        {
            var categoriaDominio = await _categoriaRepositorio.ObterPorIdAsync(id);

            if (categoriaDominio == null)
                throw new Exception("Categoria não encontrada");

            return categoriaDominio;
        }

        public async Task<IEnumerable<Categoria>> ListarCategoriasAsync(int usauarioId, bool ativo)
        {
            var categorias = await _categoriaRepositorio.ListarAsync(usauarioId, ativo);

            if (categorias.Count() <= 0)
                throw new Exception("A lista está vazia");

            return categorias;
        }
    }
}