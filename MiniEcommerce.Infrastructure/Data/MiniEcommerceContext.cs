using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Infrastructure.Data
{
    public class MiniEcommerceContext : DbContext
    {
        public MiniEcommerceContext(DbContextOptions<MiniEcommerceContext> options) 
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar configurações
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiniEcommerceContext).Assembly);
        }
    }
}
