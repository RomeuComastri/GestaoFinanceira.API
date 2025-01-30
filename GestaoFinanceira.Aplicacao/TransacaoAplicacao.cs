using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Enumeradores;
using GestaoFinanceira.Repositorio;

namespace GestaoFinanceira.Aplicacao
{
    public class TransacaoAplicacao : ITransacaoAplicacao
    {
        readonly ITransacaoRepositorio _transacaoRepositorio;

        public TransacaoAplicacao(ITransacaoRepositorio transacaoRepositorio)
        {
            _transacaoRepositorio = transacaoRepositorio;
        }

        public async Task<int> SalvarTransacaoAsync(Transacao transacao)
        {
            if (transacao == null)
                throw new Exception("Transação não pode ser vazio");

            if (transacao.Valor <= 0)
                throw new Exception("O valor não pode ser negativo ou igual a zero");

            if (transacao.Data == DateTime.MinValue)
                throw new Exception("A data não pode ser vazia");

            transacao.Data = transacao.Data.Date;

            return await _transacaoRepositorio.SalvarAsync(transacao);
        }
        public async Task AtualizarTransacaoAsync(Transacao transacao)
        {
            var transacaoDominio = await _transacaoRepositorio.ObterPorIdAsync(transacao.Id);

            if (transacaoDominio == null || !transacaoDominio.Status)
                throw new Exception("Transacao não encontrada");

            if (transacao.Valor > 0)
                transacaoDominio.Valor = transacao.Valor;

            if (transacao.Data != DateTime.MinValue)
                transacaoDominio.Data = transacao.Data;

            if (!string.IsNullOrEmpty(transacao.Descricao))
                transacaoDominio.Descricao = transacao.Descricao;

            if (transacao.CategoriaId > 0)
                transacaoDominio.CategoriaId = transacao.CategoriaId;

            await _transacaoRepositorio.AtualizarAsync(transacaoDominio);
        }
        public async Task DesativarTransacaoAsync(int transacaoId)
        {
            var transacaoDominio = await _transacaoRepositorio.ObterPorIdAsync(transacaoId);

            if (transacaoDominio == null)
                throw new Exception("Transacao não encontrada");

            if (!transacaoDominio.Status)
                throw new Exception("Transacao ja desativada no sistema");

            transacaoDominio.Deletar();

            await _transacaoRepositorio.AtualizarAsync(transacaoDominio);
        }

        public async Task RestaurarTransacaoAsync(int transacaoId)
        {
            var transacaoDominio = await _transacaoRepositorio.ObterPorIdAsync(transacaoId);

            if (transacaoDominio == null)
                throw new Exception("Transacao não encontrada");

            if (transacaoDominio.Status)
                throw new Exception("Transacao ja ativa no sistema");

            transacaoDominio.Restaurar();

            await _transacaoRepositorio.AtualizarAsync(transacaoDominio);
        }

        public async Task<Transacao> ObterTransacaoPorIdAsync(int id)
        {
            var transacaoDominio = await _transacaoRepositorio.ObterPorIdAsync(id);

            if (transacaoDominio == null)
                throw new Exception("transacao não encontrada");

            return transacaoDominio;
        }
        public async Task<IEnumerable<Transacao>> ListarTransacoesAsync()
        {
            var transacoes = await _transacaoRepositorio.ListarAsync();

            if (transacoes.Count() <= 0)
                throw new Exception("A lista está vazia");

            return transacoes;
        }

        public List<int> ListarTipoTransacaoNumero()
        {
            var valores = Enum.GetValues<TipoTransacao>().Cast<int>().ToList();

            return valores;
        }

        public List<string> ListarTipoTransacaoNome()
        {
            var nomes = Enum.GetNames<TipoTransacao>().ToList();
            return nomes;
        }
    }
}