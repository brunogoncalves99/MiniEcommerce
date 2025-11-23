using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Interfaces;
using MiniEcommerce.Infrastructure.Data;

namespace MiniEcommerce.Infrastructure.Repositories
{
    public class RepositorioCupom : RepositorioBase<Cupom>, IRepositorioCupom
    {
        public RepositorioCupom(MiniEcommerceContext context) : base(context)
        {
        }

        public async Task<Cupom> ObterPorCodigoAsync(string codigo)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Codigo == codigo.ToUpper() && c.Ativo);
        }

        public async Task<IEnumerable<Cupom>> ObterCuponsValidosAsync()
        {
            var dataAtual = DateTime.Now;
            return await _dbSet
                .Where(c => c.Ativo && c.DataValidade >= dataAtual)
                .ToListAsync();
        }

        public async Task<bool> CodigoExisteAsync(string codigo)
        {
            return await _dbSet.AnyAsync(c => c.Codigo == codigo.ToUpper());
        }
    }
}
