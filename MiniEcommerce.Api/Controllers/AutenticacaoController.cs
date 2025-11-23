using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;
using System.Text.Json;

namespace MiniEcommerce.Api.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly IServicoAutenticacao _servicoAutenticacao;

        public AutenticacaoController(IServicoAutenticacao servicoAutenticacao)
        {
            _servicoAutenticacao = servicoAutenticacao;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Se já estiver logado, redireciona
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { sucesso = false, mensagem = "Dados inválidos" });
                }

                var usuario = await _servicoAutenticacao.AutenticarAsync(loginDto);

                if (usuario == null)
                {
                    return Json(new { sucesso = false, mensagem = "CPF ou senha inválidos" });
                }

                // Salvar usuário na sessão
                var usuarioJson = JsonSerializer.Serialize(new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Cpf,
                    usuario.Email,
                    Perfil = usuario.Perfil.ToString()
                });

                HttpContext.Session.SetString("Usuario", usuarioJson);

                return Json(new { sucesso = true, mensagem = "Login realizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = $"Erro ao fazer login: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
