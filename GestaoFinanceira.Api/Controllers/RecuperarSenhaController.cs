using Microsoft.AspNetCore.Mvc;
using GestaoFinanceira.Api.Models;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Servicos.Interfaces;
using System;
using System.Threading.Tasks;

namespace Projeto360.Api
{
    [ApiController]
    [Route("[controller]")]
    public class RecuperarSenhaController : ControllerBase
    {
        private readonly IRecuperarSenhaAplicacao _recuperarSenhaAplicacao;

        public RecuperarSenhaController(IRecuperarSenhaAplicacao recuperarSenhaAplicacao)
        {
            _recuperarSenhaAplicacao = recuperarSenhaAplicacao;
        }

        [HttpPost]
        [Route("EnviarEmail")]
        public async Task<ActionResult> EnviarEmailAsync([FromBody] RecuperarSenhaRequisicao recuperarSenhaRequisicao)
        {
            try
            {
                await _recuperarSenhaAplicacao.EnviarEmailAsync(recuperarSenhaRequisicao.Email);

                return Ok("E-mail enviado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("EnviarCodigo")]
        public async Task<ActionResult> EnviarCodigoAsync([FromBody] ValidarCodigoRequisicao validarCodigoRequisicao)
        {
            try
            {
                await _recuperarSenhaAplicacao.VerificarCodigoAsync(validarCodigoRequisicao.Email, validarCodigoRequisicao.codigo);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AlterarSenha")]
        public async Task<ActionResult> AlterarSenhaAsync([FromBody] AlterarSenha alterarSenha)
        {
            try
            {
                var usuarioDominio = new Usuario()
                {
                    Email = alterarSenha.Email,
                    Senha = alterarSenha.NovaSenha
                };

                await _recuperarSenhaAplicacao.AlterarSenhaAsync(usuarioDominio);

                return Ok("Senha alterada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
