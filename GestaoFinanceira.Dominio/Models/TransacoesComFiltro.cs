namespace GestaoFinanceira.Dominio.Models
{
    public class TransacoesComFiltro
    {
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public string NomeCategoria { get; set; }
        public DateTime Data { get; set; }
        public Int32 TransacaoID { get; set; }
        public Int32 CategoriaID { get; set; }
        public bool Status { get; set; }
    }
}