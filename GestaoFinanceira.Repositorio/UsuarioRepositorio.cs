using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Repositorio.Contexto;

namespace GestaoFinanceira.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(GestaoFinanceiraContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();

            return usuario.Id;
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            return await _contexto.Usuarios.Where(usuario => usuario.Status == true).FirstOrDefaultAsync(usuario => usuario.Id == id);
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _contexto.Usuarios.Where(usuario => usuario.Status == true).FirstOrDefaultAsync(usuario => usuario.Email == email);
        }

        public async Task<IEnumerable<Usuario>> ListarAsync()
        {
            return await _contexto.Usuarios.ToListAsync();
        }
    }
}