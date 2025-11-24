using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Application.DTOs;
using System.Text.Json;

namespace MiniEcommerce.Api.Controllers
{
    public class CarrinhoController : Controller
    {
        private const string CARRINHO_SESSION_KEY = "Carrinho";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Adicionar itens no carrinho
        [HttpPost]
        public IActionResult Adicionar([FromBody] ItemCarrinhoDTO item)
        {
            try
            {
                var carrinho = ObterCarrinho();
                
                var itemExistente = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == item.ProdutoId);
                
                if (itemExistente != null)
                {
                    itemExistente.Quantidade += item.Quantidade;
                }
                else
                {
                    carrinho.Itens.Add(item);
                }

                CalcularTotais(carrinho);
                SalvarCarrinho(carrinho);

                return Json(new { sucesso = true, mensagem = "Produto adicionado ao carrinho!", dados = carrinho });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }
        #endregion

        #region Remover itens do carrinho

        [HttpPost]
        public IActionResult Remover([FromBody] int produtoId)
        {
            try
            {
                var carrinho = ObterCarrinho();
                var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
                
                if (item != null)
                {
                    carrinho.Itens.Remove(item);
                    CalcularTotais(carrinho);
                    SalvarCarrinho(carrinho);
                }

                return Json(new { sucesso = true, mensagem = "Produto removido do carrinho!", dados = carrinho });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        #endregion

        #region Atualizar quantidade de produtos

        [HttpPost]
        public IActionResult AtualizarQuantidade([FromBody] dynamic dados)
        {
            try
            {
                int produtoId = dados.produtoId;
                int quantidade = dados.quantidade;

                var carrinho = ObterCarrinho();
                var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
                
                if (item != null)
                {
                    item.Quantidade = quantidade;
                    CalcularTotais(carrinho);
                    SalvarCarrinho(carrinho);
                }

                return Json(new { sucesso = true, dados = carrinho });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        #endregion

        [HttpGet]
        public IActionResult Obter()
        {
            try
            {
                var carrinho = ObterCarrinho();
                return Json(new { sucesso = true, dados = carrinho });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Limpar()
        {
            try
            {
                HttpContext.Session.Remove(CARRINHO_SESSION_KEY);
                return Json(new { sucesso = true, mensagem = "Carrinho limpo!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        private CarrinhoDTO ObterCarrinho()
        {
            var carrinhoJson = HttpContext.Session.GetString(CARRINHO_SESSION_KEY);
            
            if (string.IsNullOrEmpty(carrinhoJson))
            {
                return new CarrinhoDTO();
            }

            return JsonSerializer.Deserialize<CarrinhoDTO>(carrinhoJson);
        }

        private void SalvarCarrinho(CarrinhoDTO carrinho)
        {
            var carrinhoJson = JsonSerializer.Serialize(carrinho);
            HttpContext.Session.SetString(CARRINHO_SESSION_KEY, carrinhoJson);
        }

        private void CalcularTotais(CarrinhoDTO carrinho)
        {
            carrinho.ValorSubtotal = carrinho.Itens.Sum(i => i.ValorTotal);
            carrinho.ValorTotal = carrinho.ValorSubtotal - carrinho.ValorDesconto;
        }
    }
}
