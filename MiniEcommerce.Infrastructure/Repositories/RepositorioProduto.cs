using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Interfaces;
using MiniEcommerce.Infrastructure.Data;

namespace MiniEcommerce.Infrastructure.Repositories
{
    public class RepositorioProduto : RepositorioBase<Produto>, IRepositorioProduto
    {
        public RepositorioProduto(MiniEcommerceContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoriaAsync(string categoria)
        {
            return await _dbSet
                .Where(p => p.Categoria == categoria && p.Ativo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterComEstoqueAsync()
        {
            return await _dbSet
                .Where(p => p.QuantidadeEstoque > 0 && p.Ativo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> BuscarPorNomeAsync(string nome)
        {
            return await _dbSet
                .Where(p => p.Nome.Contains(nome) && p.Ativo)
                .ToListAsync();
        }
    }
}
