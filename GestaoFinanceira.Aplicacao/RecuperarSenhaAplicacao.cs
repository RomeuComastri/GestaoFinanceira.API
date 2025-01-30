using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;
using GestaoFinanceira.Servicos.Interfaces;


namespace GestaoFinanceira.Aplicacao
{
    public class RecuperarSenhaAplicacao : IRecuperarSenhaAplicacao
    {
        readonly IRecuperarSenhaRepositorio _recuperarSenhaRepositorio;
        readonly IRecuperarSenhaServico _recuperarSenhaServico;
        readonly IUsuarioRepositorio _usuarioRepositorio;
        readonly IUsuarioAplicacao _usuarioAplicacao;

        public RecuperarSenhaAplicacao(IRecuperarSenhaRepositorio recuperarSenhaRepositorio, IRecuperarSenhaServico recuperarSenhaServico, IUsuarioRepositorio usuarioRepositorio, IUsuarioAplicacao usuarioAplicacao)
        {
            _recuperarSenhaRepositorio = recuperarSenhaRepositorio;
            _recuperarSenhaServico = recuperarSenhaServico;
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioAplicacao = usuarioAplicacao;
        }

        public async Task EnviarEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("O email não pode ser nulo ou vazio");
            }

            var emailExiste = await _usuarioRepositorio.ObterPorEmailAsync(email);

            if (emailExiste == null)
            {
                throw new Exception("Email de usuario não cadastrado no sistema");
            }

            Random random = new Random();
            string codigo = random.Next(100000, 999999).ToString();

            await _recuperarSenhaServico.EnviarEmailRecuperacaoAsync(email, codigo);

            var recuperarSenhaDominio = new RecuperarSenha()
            {
                Email = email,
                Codigo = codigo
            };

            await SalvarDadosAsync(recuperarSenhaDominio);
        }

        public async Task SalvarDadosAsync(RecuperarSenha recuperarSenha)
        {
            await _recuperarSenhaRepositorio.SalvarAsync(recuperarSenha);
        }

        public async Task VerificarCodigoAsync(string email, string codigo)
        {
            var validarRecuperarSenha = await _recuperarSenhaRepositorio.ObterPorEmailECodigoAsync(email, codigo);

            if (validarRecuperarSenha == null)
            {
                throw new Exception("Código invalido");
            }

            validarRecuperarSenha.Ativo = false;
            await _recuperarSenhaRepositorio.AtualizarAsync(validarRecuperarSenha);
        }

        public async Task AlterarSenhaAsync(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new Exception("A senha não pode sem nula ou vazia");
            }

            var usuarioDominio = await _usuarioRepositorio.ObterPorEmailAsync(usuario.Email);

            if (usuarioDominio == null)
            {
                throw new Exception("usuario não cadastrado no sistema!");
            }

            var usuarioFezValidacao = await _recuperarSenhaRepositorio.ObterDesativadoAsync(usuarioDominio.Email);

            if (usuarioFezValidacao == null)
            {
                throw new Exception("Para alterar a senha é necessario fazer a validação");
            }

            var senhaCriptografada = _usuarioAplicacao.CriptografiaDeSenha(usuario.Senha);

            usuarioDominio.Senha = senhaCriptografada;

            await _usuarioRepositorio.AtualizarAsync(usuarioDominio);

            usuarioFezValidacao.SenhaAlterada = true;

            await _recuperarSenhaRepositorio.AtualizarAsync(usuarioFezValidacao);
        }
    }
}
