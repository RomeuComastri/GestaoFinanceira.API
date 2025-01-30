using GestaoFinanceira.Dominio.Entidades;

public interface ITransacaoRepositorio
{
    Task<int> SalvarAsync(Transacao transacao);
    Task AtualizarAsync(Transacao transacao);
    Task<Transacao> ObterPorIdAsync(int id);
    Task<IEnumerable<Transacao>> ListarAsync();
}