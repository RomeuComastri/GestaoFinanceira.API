using GestaoFinanceira.Dominio.Enumeradores;

namespace GestaoFinanceira.Api.Models
{
    public class TransacoesComFiltroRequisicao
    {
        public int UsuarioId { get; set; }
        public TipoTransacao? Tipo { get; set; }
        public int? CategoriaId { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}