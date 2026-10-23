
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GerenciadorAcademico.ViewModels;
using GerenciadorAcademico.Services;

namespace GerenciadorAcademico.Controllers;

public class ProjetosController : Controller
{
    private readonly IProjetoService _projetoService;

    public ProjetosController(IProjetoService projetoService)
    {
        _projetoService = projetoService;
    }

    // GET: Abre a tela de cadastro vazia
    [HttpGet]
    public async Task<IActionResult> Cadastrar()
    {
        var professores = await _projetoService.ListarProfessoresAsync();

        ViewBag.Professores = new SelectList(
            professores,
            "Id",
            "Nome"
        );

        return View(new NovoProjetoViewModel());
    }

    // POST: Recebe o formulário preenchido
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(NovoProjetoViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _projetoService.AdicionarAsync(model);
            return RedirectToAction(nameof(Index));
        }

        var professores = await _projetoService.ListarProfessoresAsync();

        ViewBag.Professores = new SelectList(
            professores,
            "Id",
            "Nome"
        );

        return View(model);
    }

    // INDEX: Lista todos os projetos
    public async Task<IActionResult> Index()
    {
        var projetos = await _projetoService.ListarAsync();

        var model = new ProjetosIndexViewModel
        {
            projetos = projetos,
            textopesquisa = "",
            quantidadetotal = projetos.Count(),
            ordenarPor = ""
        };

        return View(model);
    }
}
