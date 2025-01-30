using GestaoFinanceira.Repositorio;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;

namespace GestaoFinanceira.Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<int> VerificarLoginUsuario(string email, string senha)
        {
            var usuario = await _usuarioRepositorio.ObterPorEmailAsync(email);

            if (usuario == null)
            {
                throw new Exception("Usuario não encontrado");
            }

            if (usuario.Senha != senha)
            {
                throw new Exception("Senha Incorreta");
            }

            return usuario.Id;
        }

        public async Task<int> SalvarUsuarioAsync(Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("O usuário não pode ser vazio");

            if (string.IsNullOrEmpty(usuario.Nome))
                throw new Exception("O nome não pode ser vazio");

            if (string.IsNullOrEmpty(usuario.Email))
                throw new Exception("O e-mail não pode ser vazio");

            if (string.IsNullOrEmpty(usuario.Senha))
                throw new Exception("A senha não pode ser vazia");

            var usuarioExiste = await _usuarioRepositorio.ObterPorEmailAsync(usuario.Email);

            if (usuarioExiste != null)
                throw new Exception("Endereço de e-mail ja cadastrado no sistema");

            return await _usuarioRepositorio.SalvarAsync(usuario);
        }
        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nome) && string.IsNullOrEmpty(usuario.Email))
                throw new Exception("Todos os campos estão vazios, preencha algum para atualizar");

            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuario.Id);

            if (usuarioDominio == null || !usuarioDominio.Status)
                throw new Exception("Usuário não encontrado");

            if (!string.IsNullOrEmpty(usuario.Email) && usuarioDominio.Email != usuario.Email)
            {
                var usuarioVerificarEmail = await _usuarioRepositorio.ObterPorEmailAsync(usuario.Email);

                if (usuarioVerificarEmail != null)
                    throw new Exception("Endereço de e-mail ja cadastrado no sistema, por favor tente novamente");
            }

            usuarioDominio.Nome = string.IsNullOrEmpty(usuario.Nome) ? usuarioDominio.Nome : usuario.Nome;
            usuarioDominio.Email = string.IsNullOrEmpty(usuario.Email) ? usuarioDominio.Email : usuario.Email;

            await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
        }
        public async Task AlterarSenhaUsuarioAsync(Usuario usuario, string senhaAntiga)
        {
            if (string.IsNullOrEmpty(usuario.Senha))
                throw new Exception("A nova senha não pode ser vazia");

            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuario.Id);

            if (usuarioDominio == null || !usuarioDominio.Status)
                throw new Exception("Usuario não encontrado");

            if (usuarioDominio.Senha != senhaAntiga)
                throw new Exception("Senha antiga incorreta");

            usuarioDominio.Senha = usuario.Senha;

            await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
        }
        public async Task DesativarUsuarioAsync(int usuarioId)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuarioId);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado no sistema");

            if (!usuarioDominio.Status)
                throw new Exception("Usuario ja desativado no sistema");

            usuarioDominio.Deletar();

            await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
        }
        public async Task RestaurarUsuarioAsync(int usuarioId)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuarioId);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado no sistema");

            if (usuarioDominio.Status)
                throw new Exception("Usuario ja ativo no sistema");

            usuarioDominio.Restaurar();

            await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
        }
        public async Task<Usuario> ObterUsuarioPorIdAsync(int usuarioId)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuarioId);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado");

            return usuarioDominio;
        }
        public async Task<IEnumerable<Usuario>> ListarUsuariosAsync()
        {
            var usuarios = await _usuarioRepositorio.ListarAsync();

            if (usuarios.Count() <= 0)
                throw new Exception("A lista está vazia");

            return usuarios;
        }
    }
}