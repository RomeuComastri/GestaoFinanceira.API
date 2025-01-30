using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoFinanceira.Dominio.Entidades;

namespace GestaoFinanceira.Repositorio.Configuracoes
{
    public class CategoriaConfiguracoes : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias").HasKey(categoria => categoria.Id);

            builder.Property(nameof(Categoria.Id)).HasColumnName("CategoriaID");
            builder.Property(nameof(Categoria.UsuarioId)).HasColumnName("UsuarioID");
            builder.Property(nameof(Categoria.Tipo)).HasColumnName("Tipo").HasConversion<string>().IsRequired(true);
            builder.Property(nameof(Categoria.Nome)).HasColumnName("Nome").IsRequired(true);
            builder.Property(nameof(Categoria.Status)).HasColumnName("Status").IsRequired(true);

            builder.HasOne(nameof(Categoria.Usuario))
                   .WithMany(nameof(Usuario.Categorias))
                   .HasForeignKey(nameof(Categoria.UsuarioId));
        }
    }
}

