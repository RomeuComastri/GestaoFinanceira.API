using GestaoFinanceira.Dominio.Entidades;
public interface IRecuperarSenhaRepositorio
    {
    Task<int> SalvarAsync(RecuperarSenha recuperarSenha);
    Task AtualizarAsync(RecuperarSenha recuperarSenha);
    Task<RecuperarSenha> ObterDesativadoAsync(string email);
    Task<RecuperarSenha> ObterPorEmailECodigoAsync(string email, string codigo);
    Task<IEnumerable<RecuperarSenha>> ListarAsync();
}
