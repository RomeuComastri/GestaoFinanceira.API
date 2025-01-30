namespace GestaoFinanceira.Api.Models
{
    public class UsuarioAlterarSenha
    {
        public int Id { get; set; }
        public string Senha { get; set; }
        public string SenhaAntiga { get; set; }
    }
}