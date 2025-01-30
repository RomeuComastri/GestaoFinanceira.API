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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaAplicacao _categoriaAplicacao;

        public CategoriaController(ICategoriaAplicacao categoriaAplicacao)
        {
            _categoriaAplicacao = categoriaAplicacao;
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] CategoriaCriar categoriaCriar)
        {
            try
            {
                var categoriaDominio = new Categoria()
                {
                    Tipo = categoriaCriar.Tipo,
                    Nome = categoriaCriar.Nome,
                    UsuarioId = categoriaCriar.UsuarioId
                };

                var categoriaId = await _categoriaAplicacao.SalvarCategoriaAsync(categoriaDominio);

                return Ok(categoriaId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obter/{categoriaId}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int categoriaId)
        {
            try
            {
                var categoriaDominio = await _categoriaAplicacao.ObterCategoriaPorIdAsync(categoriaId);

                var categoriaResposta = new CategoriaResposta
                {
                    Id = categoriaDominio.Id,
                    Nome = categoriaDominio.Nome,
                    Tipo = categoriaDominio.Tipo,
                    UsuarioId = categoriaDominio.UsuarioId
                };

                return Ok(categoriaResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] CategoriaAtualizar categoriaAtualizar)
        {
            try
            {
                var categoriaDominio = new Categoria()
                {
                    Id = categoriaAtualizar.Id,
                    Nome = categoriaAtualizar.Nome
                };

                await _categoriaAplicacao.AtualizarCategoriaAsync(categoriaDominio);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{categoriaId}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int categoriaId)
        {
            try
            {
                await _categoriaAplicacao.DesativarCategoriaAsync(categoriaId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Restaurar/{categoriaId}")]
        public async Task<ActionResult> RestaurarAsync([FromRoute] int categoriaId)
        {
            try
            {
                await _categoriaAplicacao.RestaurarCategoriaAsync(categoriaId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar/{usuarioId}/{ativo}")]
        public async Task<ActionResult> ListAsync([FromRoute] int usuarioId, bool ativo)
        {
            try
            {
                var categoriasDominio = await _categoriaAplicacao.ListarCategoriasAsync(usuarioId, ativo);

                var categorias = categoriasDominio.Select(categoria => new CategoriaResposta()
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Tipo = categoria.Tipo,
                    UsuarioId = categoria.UsuarioId
                }).ToList();

                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}