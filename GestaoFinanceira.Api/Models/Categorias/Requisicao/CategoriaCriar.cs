using GestaoFinanceira.Dominio.Enumeradores;
namespace GestaoFinanceira.Api.Models
{
    public class CategoriaCriar
    {
        public TipoTransacao Tipo { get; set; }
        public string Nome { get; set; }
        public int UsuarioId { get; set; }
    }
}