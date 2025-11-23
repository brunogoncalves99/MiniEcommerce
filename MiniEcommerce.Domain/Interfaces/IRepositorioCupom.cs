using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Domain.Interfaces
{
    public interface IRepositorioCupom : IRepositorioBase<Cupom>
    {
        Task<Cupom> ObterPorCodigoAsync(string codigo);
        Task<IEnumerable<Cupom>> ObterCuponsValidosAsync();
        Task<bool> CodigoExisteAsync(string codigo);
    }
}
