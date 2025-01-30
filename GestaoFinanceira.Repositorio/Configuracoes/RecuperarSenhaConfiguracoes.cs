using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoFinanceira.Dominio.Entidades;

namespace GestaoFinanceira.Repositorio.Configuracoes
{
    public class RecuperarSenhaConfiguracoes : IEntityTypeConfiguration<RecuperarSenha>
    {
        public void Configure(EntityTypeBuilder<RecuperarSenha> builder)
        {
            builder.ToTable("RecuperarSenhas").HasKey(recuperarSenha => recuperarSenha.Id);

            builder.Property(nameof(RecuperarSenha.Id)).HasColumnName("RecuperarSenhaID");
            builder.Property(nameof(RecuperarSenha.Email)).HasColumnName("Email").IsRequired(true);
            builder.Property(nameof(RecuperarSenha.Codigo)).HasColumnName("Codigo").IsRequired(true);
            builder.Property(nameof(RecuperarSenha.DataEnvio)).HasColumnName("DataEnvio").IsRequired(true);
            builder.Property(nameof(RecuperarSenha.DataExpiracao)).HasColumnName("DataExpiracao").IsRequired(true);
            builder.Property(nameof(RecuperarSenha.Ativo)).HasColumnName("Ativo").IsRequired(true);
            builder.Property(nameof(RecuperarSenha.SenhaAlterada)).HasColumnName("SenhaAlterada").IsRequired(true);
        }
    }
}
