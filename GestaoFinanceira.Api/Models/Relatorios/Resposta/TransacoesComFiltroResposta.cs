using GestaoFinanceira.Dominio.Enumeradores;

namespace GestaoFinanceira.Api.Models
{
    public class TransacoesComFiltroResposta
    {
        public TipoTransacao? Tipo { get; set; }
        public decimal Valor { get; set; }
        public string NomeCategoria { get; set; }
        public DateTime Data { get; set; }
        public Int32 TransacaoId { get; set; }
        public Int32 CategoriaId { get; set; }
        public bool Status { get; set; }
    }
}