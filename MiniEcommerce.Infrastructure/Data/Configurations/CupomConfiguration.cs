using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Infrastructure.Data.Configurations
{
    public class CupomConfiguration : IEntityTypeConfiguration<Cupom>
    {
        public void Configure(EntityTypeBuilder<Cupom> builder)
        {
            builder.ToTable("Cupons");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(c => c.Codigo)
                .IsUnique();

            builder.Property(c => c.PercentualDesconto)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(c => c.ValorMaximoDesconto)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.ValorMinimoCompra)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.DataValidade)
                .IsRequired();

            builder.Property(c => c.QuantidadeUsada)
                .IsRequired();

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .IsRequired();

            builder.HasMany(c => c.Pedidos)
                .WithOne(p => p.Cupom)
                .HasForeignKey(p => p.CupomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
