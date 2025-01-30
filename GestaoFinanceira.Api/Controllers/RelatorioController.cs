using Microsoft.AspNetCore.Mvc;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;
using GestaoFinanceira.Aplicacao;
using Microsoft.EntityFrameworkCore.Storage;
using System.Windows.Markup;
using GestaoFinanceira.Api.Models;
using GestaoFinanceira.Dominio.Models;

namespace GestaoFinanceira.Api
{
    [ApiController]
    [Route("[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioAplicacao _relatorioAplicacao;

        public RelatorioController(IRelatorioAplicacao relatorioAplicacao)
        {
            _relatorioAplicacao = relatorioAplicacao;
        }

        [HttpGet]
        [Route("ObterSaldoTotal")]
        public async Task<ActionResult> ObterSaldoTotalAsync([FromQuery] SaldoTotalRequisicao saldoTotalRequisicao)
        {
            try
            {
                var saldo = await _relatorioAplicacao.ObterSaldoTotalAsync(saldoTotalRequisicao.UsuarioId, saldoTotalRequisicao.DataInicio, saldoTotalRequisicao.DataFim, saldoTotalRequisicao.CategoriaID, saldoTotalRequisicao.Tipo?.ToString());

                var saldoResposta = new SaldoTotalResposta()
                {
                    TotalDespesas = saldo.TotalDespesas,
                    TotalReceitas = saldo.TotalReceitas,
                    SaldoTotal = saldo.SaldoTotal
                };

                return Ok(saldoResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObterTransacoesFiltradas")]
        public async Task<ActionResult> ObterTransacoesFiltradas([FromQuery] TransacoesComFiltroRequisicao transacoesComFiltroRequisicao)
        {
            try
            {
                var transacoes = await _relatorioAplicacao.ObterTransacoesComFiltroAsync(transacoesComFiltroRequisicao.UsuarioId, transacoesComFiltroRequisicao.Tipo?.ToString(), transacoesComFiltroRequisicao.CategoriaId, transacoesComFiltroRequisicao.DataInicio, transacoesComFiltroRequisicao.DataFim, true);

                var transacoesResposta = transacoes.Select(t => new TransacoesComFiltroResposta
                {
                    Tipo = Enum.TryParse<TipoTransacao>(t.Tipo, out var tipo) ? tipo : (TipoTransacao?)null,
                    Valor = t.Valor,
                    NomeCategoria = t.NomeCategoria,
                    Data = t.Data,
                    TransacaoId = t.TransacaoID,
                    CategoriaId = t.CategoriaID,
                    Status= t.Status
                }).ToList();

                if (!transacoesResposta.Any())
                {
                    return NotFound("Nenhuma transação encontrada com os filtros especificados.");
                }

                return Ok(transacoesResposta);
            }
            catch (Exception ex)
            {
                // Em caso de erro, retorna uma mensagem de erro
                return BadRequest($"Erro ao obter transações: {ex.Message}");
            }
        }

    }
}