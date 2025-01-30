using GestaoFinanceira.Dominio.Enumeradores;

namespace GestaoFinanceira.Dominio.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        public TipoTransacao Tipo { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public List<Transacao> Transacoes { get; set; }

        public Categoria()
        {
            Status = true;
            Transacoes = new List<Transacao>();
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