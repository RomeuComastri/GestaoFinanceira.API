using GestaoFinanceira.Repositorio;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;
using System;
using System.Security.Cryptography;
using System.Text;


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

            var permissao = VerificarSenha(senha, usuario.Senha);

            if (!permissao)
                throw new Exception("Senha invalida");

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

            var senhaCriptografada = CriptografiaDeSenha(usuario.Senha);

            usuario.Senha = senhaCriptografada;

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
        public async Task AlterarSenhaUsuarioAsync(Usuario usuario, string senhaAtual)
        {
            if (string.IsNullOrEmpty(usuario.Senha))
                throw new Exception("A nova senha não pode ser vazia");

            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuario.Id);

            if (usuarioDominio == null || !usuarioDominio.Status)
                throw new Exception("Usuario não encontrado");

            var permissao = VerificarSenha(senhaAtual, usuarioDominio.Senha);

            if (!permissao)
                throw new Exception("Senha atual invalida");

            var novaSenhaCriptografada = CriptografiaDeSenha(usuario.Senha);

            usuarioDominio.Senha = novaSenhaCriptografada;

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

        public string CriptografiaDeSenha(string senha)
        {
            int TamanhoSalt = 16;
            int TamanhoHash = 32;
            int NumeroIteracoes = 10000;

            // Gera o salt.
            byte[] salt = new byte[TamanhoSalt];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            // Deriva o hash da senha usando PBKDF2.
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, NumeroIteracoes, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(TamanhoHash);

                // Combina o salt e o hash para armazenar.
                byte[] hashCompleto = new byte[TamanhoSalt + TamanhoHash];
                Array.Copy(salt, 0, hashCompleto, 0, TamanhoSalt);
                Array.Copy(hash, 0, hashCompleto, TamanhoSalt, TamanhoHash);

                // Retorna o resultado em Base64.
                return Convert.ToBase64String(hashCompleto);
            }
        }

        public bool VerificarSenha(string senhaVerificacao, string senhaArmazenada)
        {
            int TamanhoSalt = 16;
            int TamanhoHash = 32;
            int NumeroIteracoes = 10000;

            // Converte o hash armazenado de volta para bytes.
            byte[] hashCompleto = Convert.FromBase64String(senhaArmazenada);

            // Extrai o salt do hash armazenado.
            byte[] salt = new byte[TamanhoSalt];
            Array.Copy(hashCompleto, 0, salt, 0, TamanhoSalt);

            // Recalcula o hash da senha fornecida usando o salt extraído.
            using (var pbkdf2 = new Rfc2898DeriveBytes(senhaVerificacao, salt, NumeroIteracoes, HashAlgorithmName.SHA256))
            {
                byte[] hashCalculado = pbkdf2.GetBytes(TamanhoHash);

                // Compara os hashes byte a byte.
                for (int i = 0; i < TamanhoHash; i++)
                {
                    if (hashCompleto[TamanhoSalt + i] != hashCalculado[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}