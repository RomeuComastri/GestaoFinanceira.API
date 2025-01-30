using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Repositorio.Contexto;

namespace GestaoFinanceira.Repositorio
{
    public class CategoriaRepositorio : BaseRepositorio, ICategoriaRepositorio
    {
        public CategoriaRepositorio(GestaoFinanceiraContexto contexto) : base(contexto)
        {

        }

        public async Task<int> SalvarAsync(Categoria categoria)
        {
            _contexto.Categorias.Add(categoria);
            await _contexto.SaveChangesAsync();

            return categoria.Id;
        }

        public async Task AtualizarAsync(Categoria categoria)
        {
            _contexto.Categorias.Update(categoria);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Categoria> ObterPorIdAsync(int id)
        {
            return await _contexto.Categorias.FirstOrDefaultAsync(Categoria => Categoria.Id == id);
        }

        public async Task<IEnumerable<Categoria>> ListarAsync(int usuarioId, bool ativo)
        {
            return await _contexto.Categorias.Where(c => c.UsuarioId == usuarioId && c.Status == ativo).ToListAsync();
        }
    }
}