namespace GestaoFinanceira.Api.Models
{
    public class TransacaoCriar
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int CategoriaId { get; set; }
    }
}