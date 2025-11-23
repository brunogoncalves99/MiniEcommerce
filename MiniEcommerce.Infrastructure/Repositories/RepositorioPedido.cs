using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Enums;
using MiniEcommerce.Domain.Interfaces;
using MiniEcommerce.Infrastructure.Data;

namespace MiniEcommerce.Infrastructure.Repositories
{
    public class RepositorioPedido : RepositorioBase<Pedido>, IRepositorioPedido
    {
        public RepositorioPedido(MiniEcommerceContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pedido>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _dbSet
                .Include(p => p.Usuario)
                .Include(p => p.Cupom)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Where(p => p.UsuarioId == usuarioId && p.Ativo)
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> ObterPorStatusAsync(StatusPedido status)
        {
            return await _dbSet
                .Include(p => p.Usuario)
                .Include(p => p.Cupom)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Where(p => p.Status == status && p.Ativo)
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync();
        }

        public async Task<Pedido> ObterComItensAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Usuario)
                .Include(p => p.Cupom)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
        }

        public async Task<IEnumerable<Pedido>> ObterTodosComItensAsync()
        {
            return await _dbSet
                .Include(p => p.Usuario)
                .Include(p => p.Cupom)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Where(p => p.Ativo)
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync();
        }
    }
}
