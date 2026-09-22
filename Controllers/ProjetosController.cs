using gerenciadoracademico.services;
using GerenciadorAcademico.Models;
using GerenciadorAcademico.Services;
using GerenciadorAcademico.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciadorAcademico.Controllers;

public class ProjetosController : Controller
{
    private readonly IProjetoService _projetoService;
    private readonly IProfessorService _professorService;

    public ProjetosController(
        IProjetoService projetoService,
        IProfessorService professorService)
    {
        _projetoService = projetoService;
        _professorService = professorService;
    }

    // LISTAR PROJETOS
    [HttpGet]
    public async Task<IActionResult> Index(string? pesquisa, string? ordenarPor)
    {
        var projetos = await _projetoService.PesquisarPorTituloAsync(pesquisa);
        projetos = _projetoService.Ordenar(projetos, ordenarPor);

        foreach (var projeto in projetos)
        {
            projeto.Professor = await _professorService.ObterPorIdAsync(projeto.ProfessorId);
        }

        var model = new ProjetosIndexViewModel
        {
            projetos = projetos,
            textopesquisa = pesquisa,
            quantidadetotal = projetos.Count,
            ordenarPor = ordenarPor
        };

        return View(model);
    }

    // VER DETALHES
    [HttpGet]
    public async Task<IActionResult> Detalhes(int id)
    {
        var projeto = await _projetoService.ObterPorIdAsync(id);

        if (projeto is null)
            return NotFound();

        projeto.Professor = await _professorService.ObterPorIdAsync(projeto.ProfessorId);

        return View(projeto);
    }

    // ABRIR TELA DE CADASTRO
    [HttpGet]
    public async Task<IActionResult> Cadastrar()
    {
        var model = new NovoProjetoViewModel
        {
            Professores = await ObterListaProfessoresAsync()
        };

        return View(model);
    }

    // SALVAR CADASTRO (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(NovoProjetoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Professores = await ObterListaProfessoresAsync();
            return View(model);
        }

        await _projetoService.AdicionarAsync(model);

        TempData["Mensagem"] = "Projeto cadastrado com sucesso!";

        return RedirectToAction(nameof(Index));
    }

    // ABRIR TELA DE EDIÇÃO
    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var projeto = await _projetoService.ObterPorIdAsync(id);

        if (projeto is null)
            return NotFound();

        var model = new EditarProjetoViewModel
        {
            Id = projeto.Id,
            Titulo = projeto.Titulo,
            Descricao = projeto.Descricao,
            CargaHoraria = projeto.CargaHoraria,
            ProfessorId = projeto.ProfessorId,
            Professores = await ObterListaProfessoresAsync()
        };

        return View(model);
    }

    // SALVAR EDIÇÃO (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(EditarProjetoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Professores = await ObterListaProfessoresAsync();
            return View(model);
        }

        var atualizou = await _projetoService.AtualizarAsync(model);
        if (!atualizou)
            return NotFound();

        TempData["Mensagem"] = "Projeto updated com sucesso!";

        return RedirectToAction(nameof(Index));
    }

    // EXCLUIR PROJETO (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remover(int id)
    {
        var removeu = await _projetoService.RemoverAsync(id);
        if (!removeu)
            return NotFound();

        TempData["Mensagem"] = "Projeto removido com sucesso!";

        return RedirectToAction(nameof(Index));
    }

    // MÉTODO AUXILIAR PARA POPULAR O DROP-DOWN DE PROFESSORES
    private async Task<List<SelectListItem>> ObterListaProfessoresAsync()
    {
        // CORRIGIDO: Agora consome ListarProfessoresAsync da _projetoService conforme o Passo 2 do tutorial
        var professores = await _projetoService.ListarProfessoresAsync();

        return professores.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.Nome
        }).ToList();
    }
}
