using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Enums;

namespace MiniEcommerce.Domain.Interfaces
{
    public interface IRepositorioPedido : IRepositorioBase<Pedido>
    {
        Task<IEnumerable<Pedido>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<Pedido>> ObterPorStatusAsync(StatusPedido status);
        Task<Pedido> ObterComItensAsync(int id);
        Task<IEnumerable<Pedido>> ObterTodosComItensAsync();
    }
}
