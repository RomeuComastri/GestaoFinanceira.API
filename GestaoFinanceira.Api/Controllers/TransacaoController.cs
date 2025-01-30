using Microsoft.AspNetCore.Mvc;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;
using GestaoFinanceira.Aplicacao;
using Microsoft.EntityFrameworkCore.Storage;
using System.Windows.Markup;
using GestaoFinanceira.Api.Models;

namespace GestaoFinanceira.Api
{
    [ApiController]
    [Route("[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoAplicacao _transacaoAplicacao;

        public TransacaoController(ITransacaoAplicacao transacaoAplicacao)
        {
            _transacaoAplicacao = transacaoAplicacao;
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] TransacaoCriar transacaoCriar)
        {
            try
            {
                var transacaoDominio = new Transacao()
                {
                    Valor = transacaoCriar.Valor,
                    Descricao = transacaoCriar.Descricao,
                    Data = transacaoCriar.Data,
                    CategoriaId = transacaoCriar.CategoriaId
                };

                var usuarioId = await _transacaoAplicacao.SalvarTransacaoAsync(transacaoDominio);

                return Ok(usuarioId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obter/{transacaoId}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int transacaoId)
        {
            try
            {
                var transacaoDominio = await _transacaoAplicacao.ObterTransacaoPorIdAsync(transacaoId);

                var transacaoResposta = new TransacaoResposta
                {
                    Id = transacaoDominio.Id,
                    Valor = transacaoDominio.Valor,
                    Descricao = transacaoDominio.Descricao,
                    Data = transacaoDominio.Data,
                    CategoriaId = transacaoDominio.CategoriaId
                };

                return Ok(transacaoResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] TransacaoAtualizar transacaoAtualizar)
        {
            try
            {
                var transacaoDominio = new Transacao()
                {
                    Id = transacaoAtualizar.Id,
                    Valor = transacaoAtualizar.Valor,
                    Descricao = transacaoAtualizar.Descricao,
                    Data = transacaoAtualizar.Data,
                    CategoriaId = transacaoAtualizar.CategoriaId
                };

                await _transacaoAplicacao.AtualizarTransacaoAsync(transacaoDominio);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("Deletar/{transacaoId}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int transacaoId)
        {
            try
            {
                await _transacaoAplicacao.DesativarTransacaoAsync(transacaoId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Restaurar/{transacaoId}")]
        public async Task<ActionResult> RestaurarAsync([FromRoute] int transacaoId)
        {
            try
            {
                await _transacaoAplicacao.RestaurarTransacaoAsync(transacaoId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult> ListAsync()
        {
            try
            {
                var transacoesDominio = await _transacaoAplicacao.ListarTransacoesAsync();

                var transacoes = transacoesDominio.Select(transacao => new TransacaoResposta()
                {
                    Id = transacao.Id,
                    Valor = transacao.Valor,
                    Descricao = transacao.Descricao,
                    Data = transacao.Data,
                    CategoriaId = transacao.CategoriaId
                }).ToList();

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ListarTipoTransacoes")]
        public ActionResult ListarTipoTransacao()
        {
            try
            {
                var valores = _transacaoAplicacao.ListarTipoTransacaoNumero();
                var nomes = _transacaoAplicacao.ListarTipoTransacaoNome();

                var TipoTransacao = new List<Object>();

                for (int i = 0; i < valores.Count(); i++)
                {
                    var tipo = new
                    {
                        Id = valores[i],
                        Nome = nomes[i]
                    };
                    TipoTransacao.Add(tipo);
                }

                return Ok(TipoTransacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}