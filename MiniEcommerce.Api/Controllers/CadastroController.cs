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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] CadastroViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { sucesso = false, mensagem = "Dados inválidos" });
                }

                var usuarioDto = new UsuarioDTO
                {
                    Nome = model.Nome,
                    Cpf = model.Cpf,
                    Email = model.Email,
                    Senha = model.Senha,
                    Perfil = model.IsAdmin ? PerfilUsuario.Administrador : PerfilUsuario.Comprador,
                    Ativo = true
                };

                await _servicoUsuario.CriarAsync(usuarioDto);

                return Json(new { 
                    sucesso = true, 
                    mensagem = "Usuário cadastrado com sucesso!" 
                });
            }
            catch (Exception ex)
            {
                return Json(new { 
                    sucesso = false, 
                    mensagem = ex.Message 
                });
            }
        }
    }

    public class CadastroViewModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool IsAdmin { get; set; }
    }
}
