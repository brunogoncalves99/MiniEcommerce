using Microsoft.AspNetCore.Mvc;

namespace MiniEcommerce.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Verificar se está logado
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Login", "Autenticacao");
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
