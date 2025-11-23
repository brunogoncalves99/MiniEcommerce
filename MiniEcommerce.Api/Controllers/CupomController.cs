using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;

namespace MiniEcommerce.Api.Controllers
{
    public class CupomController : Controller
    {
        private readonly IServicoCupom _cupomService;

        public CupomController(IServicoCupom cupomService)
        {
            _cupomService = cupomService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var cupons = await _cupomService.ObterTodosAsync();
            return Json(cupons);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(int id)
        {
            var cupom = await _cupomService.ObterPorIdAsync(id);

            if (cupom == null)
                return NotFound(new { mensagem = "Cupom não encontrado" });

            return Json(cupom);
        }


        [HttpPost]
        public async Task<IActionResult> Salvar([FromBody] CupomDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensagem = "Dados inválidos" });

            try
            {
                if (dto.Id == 0)
                {
                    // Criar
                    var criado = await _cupomService.CriarAsync(dto);

                    return Ok(new
                    {
                        sucesso = true,
                        mensagem = "Cupom criado com sucesso!",
                        cupom = criado
                    });
                }
                else
                {
                    await _cupomService.AtualizarAsync(dto);

                    return Ok(new
                    {
                        sucesso = true,
                        mensagem = "Cupom atualizado com sucesso!"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    sucesso = false,
                    mensagem = "Erro ao salvar cupom: " + ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                await _cupomService.DeletarAsync(id);

                return Ok(new
                {
                    sucesso = true,
                    mensagem = "Cupom removido com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    sucesso = false,
                    mensagem = "Erro ao excluir cupom: " + ex.Message
                });
            }
        }
    }
}
