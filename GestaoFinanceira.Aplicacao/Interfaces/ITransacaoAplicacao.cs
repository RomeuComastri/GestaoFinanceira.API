using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;

public interface ITransacaoAplicacao
{
    Task<int> SalvarTransacaoAsync(Transacao transacao);
    Task AtualizarTransacaoAsync(Transacao transacao);
    Task DesativarTransacaoAsync(int transacaoId);
    Task RestaurarTransacaoAsync(int transacaoId);
    Task<Transacao> ObterTransacaoPorIdAsync(int id);
    Task<IEnumerable<Transacao>> ListarTransacoesAsync();
    List<int> ListarTipoTransacaoNumero();
    List<string> ListarTipoTransacaoNome();
}
