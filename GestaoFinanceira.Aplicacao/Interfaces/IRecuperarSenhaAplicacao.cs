using GestaoFinanceira.Dominio.Entidades;
public interface IRecuperarSenhaAplicacao
{
 Task EnviarEmailAsync(string email);
 Task SalvarDadosAsync(RecuperarSenha recuperarSenha);
    Task VerificarCodigoAsync(string email, string codigo);
    Task AlterarSenhaAsync(Usuario usuario);
}
