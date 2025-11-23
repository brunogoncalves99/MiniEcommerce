using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Domain.Interfaces
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        Task<Usuario> ObterPorCpfAsync(string cpf);
        Task<Usuario> ObterPorEmailAsync(string email);
        Task<bool> CpfExisteAsync(string cpf);
        Task<bool> EmailExisteAsync(string email);
    }
}
