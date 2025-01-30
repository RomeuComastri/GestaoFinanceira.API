using GestaoFinanceira.Repositorio;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;
using GestaoFinanceira.Dominio.Models;

namespace GestaoFinanceira.Aplicacao
{
    public class RelatorioAplicacao : IRelatorioAplicacao
    {
        readonly IRelatorioRepositorio _relatorioRepositorio;

        public RelatorioAplicacao(IRelatorioRepositorio relatorioRepositorio)
        {
            _relatorioRepositorio = relatorioRepositorio;
        }

        public async Task<SaldoToTalTransacoes> ObterSaldoTotalAsync(int usuarioId, DateTime? dataInicio = null, DateTime? dataFim = null, int? categoriaId = null, string tipo = null)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentException("O ID do usuário deve ser maior que zero.");
            }

            var saldo = await _relatorioRepositorio.SaldoTotalUsuario(usuarioId, dataInicio, dataFim, categoriaId, tipo);

            return saldo;
        }

        public async Task<IEnumerable<TransacoesComFiltro>> ObterTransacoesComFiltroAsync(int usuarioId, string tipo = null, int? categoriaId = null, DateTime? dataInicio = null, DateTime? dataFim = null, bool? status = null)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentException("O ID do usuário deve ser maior que zero.");
            }

            // Chama o repositório para obter as transações com os filtros
            var transacoes = await _relatorioRepositorio.ObterTransacoesComFiltroAsync(usuarioId, tipo, categoriaId, dataInicio, dataFim, status);

            return transacoes;
        }
    }
}