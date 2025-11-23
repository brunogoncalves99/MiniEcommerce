using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Domain.Interfaces
{
    public interface IRepositorioProduto : IRepositorioBase<Produto>
    {
        Task<IEnumerable<Produto>> ObterPorCategoriaAsync(string categoria);
        Task<IEnumerable<Produto>> ObterComEstoqueAsync();
        Task<IEnumerable<Produto>> BuscarPorNomeAsync(string nome);
    }
}
