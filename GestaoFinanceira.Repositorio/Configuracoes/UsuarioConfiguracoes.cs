using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoFinanceira.Dominio.Entidades;

namespace GestaoFinanceira.Repositorio.Configuracoes
{
    public class UsuarioConfiguracoes : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios").HasKey(usuario => usuario.Id);

            builder.Property(nameof(Usuario.Id)).HasColumnName("UsuarioID");
            builder.Property(nameof(Usuario.Nome)).HasColumnName("Nome").IsRequired(true);
            builder.Property(nameof(Usuario.Email)).HasColumnName("Email").IsRequired(true);
            builder.Property(nameof(Usuario.Senha)).HasColumnName("Senha").IsRequired(true);
            builder.Property(nameof(Usuario.Status)).HasColumnName("Status").IsRequired(true);

            builder.HasIndex(nameof(Usuario.Email)).IsUnique();
        }
    }
}

