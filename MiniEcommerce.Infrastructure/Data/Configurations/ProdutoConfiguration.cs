using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Infrastructure.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Descricao)
                .HasMaxLength(1000);

            builder.Property(p => p.Preco)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.QuantidadeEstoque)
                .IsRequired();

            builder.Property(p => p.ImagemUrl)
                .HasMaxLength(500);

            builder.Property(p => p.Categoria)
                .HasMaxLength(100);

            builder.Property(p => p.Ativo)
                .IsRequired();

            builder.Property(p => p.DataCriacao)
                .IsRequired();

            builder.HasMany(p => p.ItensPedido)
                .WithOne(i => i.Produto)
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
