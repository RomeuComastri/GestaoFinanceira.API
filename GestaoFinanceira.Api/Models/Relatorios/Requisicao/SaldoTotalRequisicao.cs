using GestaoFinanceira.Dominio.Enumeradores;

namespace GestaoFinanceira.Api.Models
{
    public class SaldoTotalRequisicao
    {
        public int UsuarioId { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int? CategoriaID { get; set; }
        public TipoTransacao? Tipo { get; set; }
    }
}