using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;
using MiniEcommerce.Domain.Enums;

namespace MiniEcommerce.Api.Controllers
{
    public class CadastroController : Controller
    {
        private readonly IServicoUsuario _servicoUsuario;

        public CadastroController(IServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        [HttpGet]
        public IActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _servicoUsuario.ObterTodosAsync();
            return Json(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuario(int id)
        {
            var usuarios = await _servicoUsuario.ObterPorIdAsync(id);

            if (usuarios == null)
                return NotFound(new { mensagem = "Usuario não encontrado" });

            return Json(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO usuario)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { sucesso = false, mensagem = "Dados inválidos" });
            }

            try
            {
                // Verificar se existe o usuario, caso não exista cria um novo usuario
                if(usuario.Id == 0)
                {
                    await _servicoUsuario.CriarAsync(usuario);

                    return Json(new
                    {
                        sucesso = true,
                        mensagem = "Usuário cadastrado com sucesso!"
                    });
                }
                else
                {
                    await _servicoUsuario.AtualizarAsync(usuario);

                    return Ok(new
                    {
                        sucesso = true,
                        mensagem = "Usuário atualizado com sucesso!"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new {
                    sucesso = false,
                    mensagem = "Erro ao criar usuário: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _servicoUsuario.DeletarAsync(id);

                return Ok(new
                {
                    sucesso = true,
                    mensagem = "Usuario deletado com sucesso!"
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(new
                {
                    sucesso = false,
                    mensagem = "Erro ao excluir usuario" + ex.Message
                });
            }
        }
    }
}
