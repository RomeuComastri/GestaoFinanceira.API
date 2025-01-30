using GestaoFinanceira.Dominio.Enumeradores;

namespace GestaoFinanceira.Api.Models
{
    public class CategoriaResposta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int UsuarioId { get; set; }
    }
}