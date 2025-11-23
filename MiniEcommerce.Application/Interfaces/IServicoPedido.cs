using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Domain.Enums;

namespace MiniEcommerce.Application.Interfaces
{
    public interface IServicoPedido
    {
        Task<PedidoDTO> ObterPorIdAsync(int id);
        Task<IEnumerable<PedidoDTO>> ObterTodosAsync();
        Task<IEnumerable<PedidoDTO>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<PedidoDTO>> ObterPorStatusAsync(StatusPedido status);
        Task<PedidoDTO> CriarPedidoAsync(CarrinhoDTO carrinho, int usuarioId);
        Task ProcessarPedidoAsync(int pedidoId);
        Task AtualizarStatusAsync(int pedidoId, StatusPedido novoStatus);
        Task MarcarComoEntregueAsync(int pedidoId);
        Task FinalizarPedidoAsync(int pedidoId);
    }
}
