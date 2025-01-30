using GestaoFinanceira.Dominio.Enumeradores;

namespace GestaoFinanceira.Api.Models
{
    public class SaldoTotalResposta
    {
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal SaldoTotal { get; set; }
    }
}