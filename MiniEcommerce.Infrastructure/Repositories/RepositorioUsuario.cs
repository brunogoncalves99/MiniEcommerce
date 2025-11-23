using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Interfaces;
using MiniEcommerce.Infrastructure.Data;

namespace MiniEcommerce.Infrastructure.Repositories
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(MiniEcommerceContext context) : base(context)
        {
        }

        public async Task<Usuario> ObterPorCpfAsync(string cpf)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Cpf == cpf && u.Ativo);
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && u.Ativo);
        }

        public async Task<bool> CpfExisteAsync(string cpf)
        {
            return await _dbSet.AnyAsync(u => u.Cpf == cpf);
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }
    }
}
