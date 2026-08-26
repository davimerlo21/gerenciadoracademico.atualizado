using System.Diagnostics;
using gerenciadoracademico.Models;
using Microsoft.AspNetCore.Mvc;

namespace gerenciadoracademico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]

        // Tela inicial (Login)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        // Recebe usuário e senha
        [HttpPost]
        public IActionResult Login(string username, string senha)
        {
            if (username == "admin" && senha == "1234")
            {
                return RedirectToAction("Index", "Projetos");
            }

            ViewBag.Erro = "Usuário ou senha inválidos";

            return View("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}