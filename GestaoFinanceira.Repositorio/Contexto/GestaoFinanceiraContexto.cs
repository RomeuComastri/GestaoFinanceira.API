using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Repositorio.Configuracoes;

namespace GestaoFinanceira.Repositorio.Contexto
{
    public class GestaoFinanceiraContexto : DbContext
    {
        public string stringConexao { get; set; } = "Server=COMASTRIROMEU\\SQLEXPRESS;Database=GestaoFinanceira;TrustServerCertificate=true;Trusted_connection=True";
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<RecuperarSenha> RecuperarSenhas { get; set; }
        private readonly DbContextOptions _options;

        public GestaoFinanceiraContexto()
        {

        }

        public GestaoFinanceiraContexto(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
                optionsBuilder.UseSqlServer(stringConexao);
        }

        public SqlConnection CriarConexao()
        {
            return new SqlConnection(stringConexao);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracoes());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguracoes());
            modelBuilder.ApplyConfiguration(new TransacaoConfiguracoes());
            modelBuilder.ApplyConfiguration(new RecuperarSenhaConfiguracoes());
        }
    }
}