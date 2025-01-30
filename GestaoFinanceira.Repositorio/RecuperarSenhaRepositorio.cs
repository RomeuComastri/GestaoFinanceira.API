using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;
using Dapper;
using GestaoFinanceira.Repositorio.Contexto;


namespace GestaoFinanceira.Repositorio
{
    public class RecuperarSenhaRepositorio : BaseRepositorio, IRecuperarSenhaRepositorio
    {
        public RecuperarSenhaRepositorio(GestaoFinanceiraContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(RecuperarSenha recuperarSenha)
        {
            _contexto.RecuperarSenhas.Add(recuperarSenha);
            await _contexto.SaveChangesAsync();

            return recuperarSenha.Id;
        }

        public async Task AtualizarAsync(RecuperarSenha recuperarSenha)
        {
            _contexto.RecuperarSenhas.Update(recuperarSenha);
            await _contexto.SaveChangesAsync();
        }

        public async Task<RecuperarSenha> ObterAsync(int recuperarSenhaId)
        {
            return await _contexto.RecuperarSenhas
                        .Where(recuperarSenha => recuperarSenha.Id == recuperarSenhaId)
                        .Where(recuperarSenha => recuperarSenha.Ativo == true)
                        .FirstOrDefaultAsync();
        }

        public async Task<RecuperarSenha> ObterDesativadoAsync(string email)
        {
            return await _contexto.RecuperarSenhas
                        .Where(recuperarSenha => recuperarSenha.Email == email)
                        .Where(recuperarSenha => recuperarSenha.Ativo == false)
                        .Where(recuperarSenha => recuperarSenha.SenhaAlterada == false)
                        .FirstOrDefaultAsync();
        }

        public async Task<RecuperarSenha> ObterPorEmailECodigoAsync(string email, string codigo)
        {
            return await _contexto.RecuperarSenhas
                        .Where(recuperarSenha => recuperarSenha.Email == email)
                        .Where(recuperarSenha => recuperarSenha.Codigo == codigo)
                        .Where(recuperarSenha => recuperarSenha.DataExpiracao > DateTime.Now)
                        .Where(recuperarSenha => recuperarSenha.Ativo == true)
                        .Where(recuperarSenha => recuperarSenha.SenhaAlterada == false)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RecuperarSenha>> ListarAsync()
        {
            return await _contexto.RecuperarSenhas.Where(recuperarSenha => recuperarSenha.Ativo == true).ToListAsync();
        }
    }
}