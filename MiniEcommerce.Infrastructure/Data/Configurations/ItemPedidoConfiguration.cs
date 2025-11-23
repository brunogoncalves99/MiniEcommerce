using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Infrastructure.Data.Configurations
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable("ItensPedido");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantidade)
                .IsRequired();

            builder.Property(i => i.ValorUnitario)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.ValorTotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.DataCriacao)
                .IsRequired();

            builder.HasOne(i => i.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Produto)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
