using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;

namespace MiniEcommerce.Api.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IServicoProduto _servicoProduto;

        public ProdutosController(IServicoProduto servicoProduto)
        {
            _servicoProduto = servicoProduto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var produtos = await _servicoProduto.ObterAtivosAsync();
                return Json(new { sucesso = true, dados = produtos });
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
                var produto = await _servicoProduto.ObterPorIdAsync(id);

                if (produto == null)
                    return Json(new { sucesso = false, mensagem = "Produto não encontrado" });

                return Json(produto);
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { sucesso = false, mensagem = "Dados inválidos" });

                var produto = await _servicoProduto.CriarAsync(produtoDto);
                return Json(new { sucesso = true, mensagem = "Produto criado com sucesso!", dados = produto });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { sucesso = false, mensagem = "Dados inválidos" });

                await _servicoProduto.AtualizarAsync(produtoDto);
                return Json(new { sucesso = true, mensagem = "Produto atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _servicoProduto.DeletarAsync(id);
                return Json(new { sucesso = true, mensagem = "Produto deletado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarEstoque(int id, int quantidade)
        {
            try
            {
                await _servicoProduto.AtualizarEstoqueAsync(id, quantidade);
                return Json(new { sucesso = true, mensagem = "Estoque atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }
    }
}