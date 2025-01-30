namespace GestaoFinanceira.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Status { get; set; }

        public List<Categoria> Categorias { get; set; }

        public Usuario()
        {
            Status = true;
            Categorias = new List<Categoria>();
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