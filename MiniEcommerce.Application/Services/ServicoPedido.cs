using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Enums;
using MiniEcommerce.Domain.Interfaces;

namespace MiniEcommerce.Application.Services
{
    public class ServicoPedido : IServicoPedido
    {
        private readonly IRepositorioPedido _repositorioPedido;
        private readonly IRepositorioProduto _repositorioProduto;
        private readonly IRepositorioCupom _repositorioCupom;
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicoPedido(
            IRepositorioPedido repositorioPedido,
            IRepositorioProduto repositorioProduto,
            IRepositorioCupom repositorioCupom,
            IRepositorioUsuario repositorioUsuario)
        {
            _repositorioPedido = repositorioPedido;
            _repositorioProduto = repositorioProduto;
            _repositorioCupom = repositorioCupom;
            _repositorioUsuario = repositorioUsuario;
        }

        public async Task<PedidoDTO> ObterPorIdAsync(int id)
        {
            var pedido = await _repositorioPedido.ObterComItensAsync(id);
            return await MapearParaDTOAsync(pedido);
        }

        public async Task<IEnumerable<PedidoDTO>> ObterTodosAsync()
        {
            var pedidos = await _repositorioPedido.ObterTodosComItensAsync();
            var pedidosDto = new List<PedidoDTO>();
            
            foreach (var pedido in pedidos)
            {
                pedidosDto.Add(await MapearParaDTOAsync(pedido));
            }
            
            return pedidosDto;
        }

        public async Task<IEnumerable<PedidoDTO>> ObterPorUsuarioAsync(int usuarioId)
        {
            var pedidos = await _repositorioPedido.ObterPorUsuarioAsync(usuarioId);
            var pedidosDto = new List<PedidoDTO>();
            
            foreach (var pedido in pedidos)
            {
                pedidosDto.Add(await MapearParaDTOAsync(pedido));
            }
            
            return pedidosDto;
        }

        public async Task<IEnumerable<PedidoDTO>> ObterPorStatusAsync(StatusPedido status)
        {
            var pedidos = await _repositorioPedido.ObterPorStatusAsync(status);
            var pedidosDto = new List<PedidoDTO>();
            
            foreach (var pedido in pedidos)
            {
                pedidosDto.Add(await MapearParaDTOAsync(pedido));
            }
            
            return pedidosDto;
        }

        public async Task<PedidoDTO> CriarPedidoAsync(CarrinhoDTO carrinho, int usuarioId)
        {
            var usuario = await _repositorioUsuario.ObterPorIdAsync(usuarioId);
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado");

            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                Usuario = usuario
            };

            // Adicionar itens ao pedido
            foreach (var item in carrinho.Itens)
            {
                var produto = await _repositorioProduto.ObterPorIdAsync(item.ProdutoId);
                
                if (produto == null)
                    throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado");

                if (!produto.TemEstoqueDisponivel(item.Quantidade))
                    throw new InvalidOperationException($"Estoque insuficiente para o produto {produto.Nome}");

                var itemPedido = new ItemPedido
                {
                    ProdutoId = produto.Id,
                    Produto = produto,
                    Quantidade = item.Quantidade,
                    ValorUnitario = produto.Preco
                };
                itemPedido.CalcularValorTotal();

                pedido.AdicionarItem(itemPedido);
                
                // Baixar estoque
                produto.BaixarEstoque(item.Quantidade);
                await _repositorioProduto.AtualizarAsync(produto);
            }

            // Aplicar cupom se houver
            if (!string.IsNullOrEmpty(carrinho.CodigoCupom))
            {
                var cupom = await _repositorioCupom.ObterPorCodigoAsync(carrinho.CodigoCupom);
                
                if (cupom != null && cupom.EstaValido())
                {
                    pedido.CupomId = cupom.Id;
                    pedido.Cupom = cupom;
                    cupom.IncrementarUso();
                    await _repositorioCupom.AtualizarAsync(cupom);
                }
            }

            pedido.CalcularValores();

            var pedidoCriado = await _repositorioPedido.AdicionarAsync(pedido);
            return await MapearParaDTOAsync(pedidoCriado);
        }

        public async Task ProcessarPedidoAsync(int pedidoId)
        {
            var pedido = await _repositorioPedido.ObterPorIdAsync(pedidoId);
            
            if (pedido == null)
                throw new InvalidOperationException("Pedido não encontrado");

            pedido.Processar();
            await _repositorioPedido.AtualizarAsync(pedido);
        }

        public async Task AtualizarStatusAsync(int pedidoId, StatusPedido novoStatus)
        {
            var pedido = await _repositorioPedido.ObterPorIdAsync(pedidoId);
            
            if (pedido == null)
                throw new InvalidOperationException("Pedido não encontrado");

            pedido.AtualizarStatus(novoStatus);
            await _repositorioPedido.AtualizarAsync(pedido);
        }

        public async Task MarcarComoEntregueAsync(int pedidoId)
        {
            var pedido = await _repositorioPedido.ObterPorIdAsync(pedidoId);
            
            if (pedido == null)
                throw new InvalidOperationException("Pedido não encontrado");

            pedido.MarcarComoEntregue();
            await _repositorioPedido.AtualizarAsync(pedido);
        }

        public async Task FinalizarPedidoAsync(int pedidoId)
        {
            var pedido = await _repositorioPedido.ObterPorIdAsync(pedidoId);
            
            if (pedido == null)
                throw new InvalidOperationException("Pedido não encontrado");

            pedido.Finalizar();
            await _repositorioPedido.AtualizarAsync(pedido);
        }

        private async Task<PedidoDTO> MapearParaDTOAsync(Pedido pedido)
        {
            if (pedido == null) return null;

            var pedidoDto = new PedidoDTO
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                NomeUsuario = pedido.Usuario?.Nome,
                CupomId = pedido.CupomId,
                CodigoCupom = pedido.Cupom?.Codigo,
                NumeroRastreio = pedido.NumeroRastreio,
                ValorSubtotal = pedido.ValorSubtotal,
                ValorDesconto = pedido.ValorDesconto,
                ValorTotal = pedido.ValorTotal,
                Status = pedido.Status,
                StatusDescricao = ObterDescricaoStatus(pedido.Status),
                DataCriacao = pedido.DataCriacao,
                Itens = new List<ItemPedidoDTO>()
            };

            if (pedido.Itens != null && pedido.Itens.Any())
            {
                foreach (var item in pedido.Itens)
                {
                    pedidoDto.Itens.Add(new ItemPedidoDTO
                    {
                        Id = item.Id,
                        ProdutoId = item.ProdutoId,
                        NomeProduto = item.Produto?.Nome,
                        ImagemProduto = item.Produto?.ImagemUrl,
                        Quantidade = item.Quantidade,
                        ValorUnitario = item.ValorUnitario,
                        ValorTotal = item.ValorTotal
                    });
                }
            }

            return pedidoDto;
        }

        private string ObterDescricaoStatus(StatusPedido status)
        {
            return status switch
            {
                StatusPedido.Pendente => "Pendente",
                StatusPedido.Processando => "Processando",
                StatusPedido.EmTransito => "Em Trânsito",
                StatusPedido.Entregue => "Entregue",
                StatusPedido.Finalizado => "Finalizado",
                StatusPedido.Cancelado => "Cancelado",
                _ => "Desconhecido"
            };
        }
    }
}
