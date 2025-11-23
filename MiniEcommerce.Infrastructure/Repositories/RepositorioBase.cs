using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Interfaces;
using MiniEcommerce.Infrastructure.Data;
using System.Linq.Expressions;

namespace MiniEcommerce.Infrastructure.Repositories
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        protected readonly MiniEcommerceContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositorioBase(MiniEcommerceContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> ObterPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await _dbSet.Where(e => e.Ativo).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> AdicionarAsync(T entidade)
        {
            await _dbSet.AddAsync(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

        public virtual async Task AtualizarAsync(T entidade)
        {
            _dbSet.Update(entidade);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeletarAsync(int id)
        {
            var entidade = await ObterPorIdAsync(id);
            if (entidade != null)
            {
                entidade.Ativo = false;
                entidade.DataAtualizacao = DateTime.Now;
                await AtualizarAsync(entidade);
            }
        }

        public virtual async Task<bool> ExisteAsync(int id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id && e.Ativo);
        }

        public virtual async Task<int> ContarAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbSet.CountAsync(e => e.Ativo);

            return await _dbSet.Where(predicate).CountAsync();
        }
    }
}
