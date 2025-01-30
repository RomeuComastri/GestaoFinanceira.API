using GestaoFinanceira.Dominio.Enumeradores;

namespace GestaoFinanceira.Dominio.Entidades
{
    public class Transacao
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public bool Status { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public Transacao()
        {
            Status = true;
        }

        public void Deletar()
        {
            Status = false;
        }

        public void Restaurar()
        {
            Status = true;
        }
    }
}