using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;
using MiniEcommerce.Domain.Enums;
using System.Text.Json;

namespace MiniEcommerce.Api.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IServicoPedido _servicoPedido;
        private readonly IServicoCupom _servicoCupom;
        private const string CARRINHO_SESSION_KEY = "Carrinho";

        public PedidosController(IServicoPedido servicoPedido, IServicoCupom servicoCupom)
        {
            _servicoPedido = servicoPedido;
            _servicoCupom = servicoCupom;
        }

        [HttpGet]
        public IActionResult TodosPedidos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MeusPedidos()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObterMeusPedidos()
        {
            try
            {
                var usuarioJson = HttpContext.Session.GetString("Usuario");
                if (string.IsNullOrEmpty(usuarioJson))
                    return Json(new { sucesso = false, mensagem = "Usuário não autenticado" });

                var usuario = JsonSerializer.Deserialize<dynamic>(usuarioJson);
                int usuarioId = usuario.GetProperty("Id").GetInt32();

                var pedidos = await _servicoPedido.ObterPorUsuarioAsync(usuarioId);
                return Json(new { sucesso = true, dados = pedidos });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var pedidos = await _servicoPedido.ObterTodosAsync();
                return Json(new { sucesso = true, dados = pedidos });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                var pedido = await _servicoPedido.ObterPorIdAsync(id);
                
                if (pedido == null)
                    return Json(new { sucesso = false, mensagem = "Pedido não encontrado" });

                return Json(new { sucesso = true, dados = pedido });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar()
        {
            try
            {
                var usuarioJson = HttpContext.Session.GetString("Usuario");
                if (string.IsNullOrEmpty(usuarioJson))
                    return Json(new { sucesso = false, mensagem = "Usuário não autenticado" });

                var usuario = JsonSerializer.Deserialize<dynamic>(usuarioJson);
                int usuarioId = usuario.GetProperty("Id").GetInt32();

                var carrinhoJson = HttpContext.Session.GetString(CARRINHO_SESSION_KEY);
                if (string.IsNullOrEmpty(carrinhoJson))
                    return Json(new { sucesso = false, mensagem = "Carrinho vazio" });

                var carrinho = JsonSerializer.Deserialize<CarrinhoDTO>(carrinhoJson);

                if (!carrinho.Itens.Any())
                    return Json(new { sucesso = false, mensagem = "Carrinho vazio" });

                // Aplicar cupom se houver
                if (!string.IsNullOrEmpty(carrinho.CodigoCupom))
                {
                    var desconto = await _servicoCupom.ValidarECalcularDescontoAsync(carrinho.CodigoCupom, carrinho.ValorSubtotal);
                    carrinho.ValorDesconto = desconto;
                    carrinho.ValorTotal = carrinho.ValorSubtotal - desconto;
                }

                var pedido = await _servicoPedido.CriarPedidoAsync(carrinho, usuarioId);

                await _servicoPedido.ProcessarPedidoAsync(pedido.Id);

                HttpContext.Session.Remove(CARRINHO_SESSION_KEY);

                return Json(new { 
                    sucesso = true, 
                    mensagem = "Pedido criado com sucesso!", 
                    dados = pedido 
                });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarStatus([FromBody] dynamic dados)
        {
            try
            {
                int pedidoId = dados.pedidoId;
                int status = dados.status;

                await _servicoPedido.AtualizarStatusAsync(pedidoId, (StatusPedido)status);
                return Json(new { sucesso = true, mensagem = "Status atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MarcarComoEntregue(int pedidoId)
        {
            try
            {
                await _servicoPedido.MarcarComoEntregueAsync(pedidoId);
                return Json(new { sucesso = true, mensagem = "Pedido marcado como entregue!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar(int pedidoId)
        {
            try
            {
                await _servicoPedido.FinalizarPedidoAsync(pedidoId);
                return Json(new { sucesso = true, mensagem = "Pedido finalizado!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AplicarCupom([FromBody] string codigoCupom)
        {
            try
            {
                var carrinhoJson = HttpContext.Session.GetString(CARRINHO_SESSION_KEY);
                if (string.IsNullOrEmpty(carrinhoJson))
                    return Json(new { sucesso = false, mensagem = "Carrinho vazio" });

                var carrinho = JsonSerializer.Deserialize<CarrinhoDTO>(carrinhoJson);

                var desconto = await _servicoCupom.ValidarECalcularDescontoAsync(codigoCupom, carrinho.ValorSubtotal);

                carrinho.CodigoCupom = codigoCupom;
                carrinho.ValorDesconto = desconto;
                carrinho.ValorTotal = carrinho.ValorSubtotal - desconto;

                var carrinhoAtualizado = JsonSerializer.Serialize(carrinho);
                HttpContext.Session.SetString(CARRINHO_SESSION_KEY, carrinhoAtualizado);

                return Json(new { 
                    sucesso = true, 
                    mensagem = "Cupom aplicado com sucesso!", 
                    dados = carrinho 
                });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }
    }
}
