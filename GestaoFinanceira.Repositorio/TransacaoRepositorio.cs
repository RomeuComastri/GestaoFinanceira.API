using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Repositorio.Contexto;

namespace GestaoFinanceira.Repositorio
{
    public class TransacaoRepositorio : BaseRepositorio, ITransacaoRepositorio
    {
        public TransacaoRepositorio(GestaoFinanceiraContexto contexto) : base(contexto)
        {

        }

        public async Task<int> SalvarAsync(Transacao transacao)
        {
            _contexto.Transacoes.Add(transacao);
            await _contexto.SaveChangesAsync();

            return transacao.Id;
        }

        public async Task AtualizarAsync(Transacao transacao)
        {
            _contexto.Transacoes.Update(transacao);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Transacao> ObterPorIdAsync(int id)
        {
            return await _contexto.Transacoes.FirstOrDefaultAsync(transacao => transacao.Id == id);
        }

        public async Task<IEnumerable<Transacao>> ListarAsync()
        {
            return await _contexto.Transacoes.ToListAsync();
        }
    }
}