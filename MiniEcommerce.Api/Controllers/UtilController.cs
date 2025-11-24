using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Application.Interfaces;

namespace MiniEcommerce.Api.Controllers
{
    public class UtilController : Controller
    {
        private readonly IServicoProduto _servicoProduto;

        public UtilController(IServicoProduto servicoProduto)
        {
            _servicoProduto = servicoProduto;
        }

        #region Metodo para correção de imagens 
        [HttpGet]
        public async Task<IActionResult> CorrigirImagensProdutos()
        {
            try
            {
                var produtos = await _servicoProduto.ObterTodosAsync();
                int corrigidos = 0;

                foreach (var produto in produtos)
                {
                    if (!string.IsNullOrEmpty(produto.ImagemUrl) && 
                        (produto.ImagemUrl.StartsWith("C:\\") || 
                         produto.ImagemUrl.StartsWith("D:\\") ||
                         produto.ImagemUrl.StartsWith("E:\\")))
                    {
                        var nomeArquivo = Path.GetFileName(produto.ImagemUrl);
                        
                        produto.ImagemUrl = $"/imagens/{nomeArquivo}";
                        
                        await _servicoProduto.AtualizarAsync(produto);
                        corrigidos++;
                    }
                }

                return Ok(new
                {
                    sucesso = true,
                    mensagem = $"{corrigidos} produtos corrigidos!",
                    total = produtos.Count()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    sucesso = false,
                    mensagem = "Erro: " + ex.Message
                });
            }
        }
        #endregion
    }
}
