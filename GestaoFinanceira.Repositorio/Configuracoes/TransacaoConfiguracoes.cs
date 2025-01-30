using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoFinanceira.Dominio.Entidades;

namespace GestaoFinanceira.Repositorio.Configuracoes
{
    public class TransacaoConfiguracoes : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes").HasKey(transacao => transacao.Id);

            builder.Property(nameof(Transacao.Id)).HasColumnName("TransacaoID");
            builder.Property(nameof(Transacao.CategoriaId)).HasColumnName("CategoriaID");
            builder.Property(nameof(Transacao.Valor)).HasColumnName("Valor").HasColumnType("decimal(18,2)").IsRequired(true);
            builder.Property(nameof(Transacao.Data)).HasColumnName("Data").HasColumnType("date").IsRequired(true);
            builder.Property(nameof(Transacao.Status)).HasColumnName("Status").IsRequired(true);
            builder.Property(nameof(Transacao.Descricao)).HasColumnName("Descricao");

            builder.HasOne(nameof(Transacao.Categoria))
                   .WithMany(nameof(Categoria.Transacoes))
                   .HasForeignKey(nameof(Transacao.CategoriaId));
        }
    }
}

