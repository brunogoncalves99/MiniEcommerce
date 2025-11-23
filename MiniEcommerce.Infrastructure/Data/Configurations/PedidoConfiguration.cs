using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Infrastructure.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.NumeroRastreio)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(p => p.NumeroRastreio)
                .IsUnique();

            builder.Property(p => p.ValorSubtotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ValorDesconto)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ValorTotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.DataCriacao)
                .IsRequired();

            builder.HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Cupom)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.CupomId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
