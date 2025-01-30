using GestaoFinanceira.Dominio.Enumeradores;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Dominio.Models;

public interface IRelatorioRepositorio
{
    Task<SaldoToTalTransacoes> SaldoTotalUsuario(int usuarioId, DateTime? dataInicio = null, DateTime? dataFim = null, int? categoriaId = null, string tipo = null);

    Task<IEnumerable<TransacoesComFiltro>> ObterTransacoesComFiltroAsync(int usuarioId, string tipo = null, int? categoriaId = null, DateTime? dataInicio = null, DateTime? dataFim = null, bool? status = null);
}

