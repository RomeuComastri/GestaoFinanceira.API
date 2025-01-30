namespace GestaoFinanceira.Api.Models
{
    public class TransacaoAtualizar
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int CategoriaId { get; set; }
    }
}