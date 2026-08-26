using gerenciadoracademico.Models;
using gerenciadoracademico.services;
using GerenciadorAcademico.ViewModels;
using GerenciadorAcademico.Services;
using GerenciadorAcademico.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gerenciadoracademico.Controllers;

public class ProjetosController : Controller
{
    private readonly IProjetoService _projetoService;
    private readonly IProfessorService _professorService;

    // Construtor recebendo ambos os serviços necessários
    public ProjetosController(IProjetoService projetoService, IProfessorService professorService)
    {
        _projetoService = projetoService;
        _professorService = professorService;
    }

    // LISTAR
    [HttpGet]
    public IActionResult Index(string? pesquisa, string? ordenarPor)
    {
        var projetos = _projetoService.PesquisarPorTitulo(pesquisa);
        projetos = _projetoService.Ordenar(projetos, ordenarPor);

        // Vincula o objeto Professor a cada projeto antes de ir para a tela
        foreach (var projeto in projetos)
        {
            projeto.Professor = _professorService.ObterPorId(projeto.ProfessorId);
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

    // DETALHES
    [HttpGet]
    public IActionResult Detalhes(int id)
    {
        var projeto = _projetoService.ObterPorId(id);

        if (projeto is null)
            return NotFound();

        projeto.Professor = _professorService.ObterPorId(projeto.ProfessorId);

        return View(projeto);
    }

    // ABRIR TELA DE CADASTRO
    [HttpGet]
    public IActionResult Cadastrar()
    {
        var model = new NovoProjetoViewModel
        {
            Professores = ObterListaProfessores()
        };

        return View(model);
    }

    // SALVAR CADASTRO
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(NovoProjetoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Professores = ObterListaProfessores();
            return View(model);
        }

        _projetoService.Adicionar(model);

        TempData["Mensagem"] = "Projeto cadastrado com sucesso!";

        return RedirectToAction(nameof(Index));
    }

    // ABRIR TELA DE EDIÇÃO
    [HttpGet]
    public IActionResult Editar(int id)
    {
        var projeto = _projetoService.ObterPorId(id);

        if (projeto is null)
            return NotFound();

        var model = new EditarProjetoViewModel
        {
            Id = projeto.Id,
            Titulo = projeto.Titulo,
            Descricao = projeto.Descricao,
            CargaHoraria = projeto.CargaHoraria,
            ProfessorId = projeto.ProfessorId,
            Professores = ObterListaProfessores()
        };

        return View(model);
    }

    // SALVAR EDIÇÃO
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(EditarProjetoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Professores = ObterListaProfessores();
            return View(model);
        }

        var updated = _projetoService.Atualizar(model);

        if (!updated)
            return NotFound();

        TempData["Mensagem"] = "Projeto updated com sucesso!";

        return RedirectToAction(nameof(Index));
    }

    // ABRIR TELA DE CONFIRMAÇÃO DE EXCLUSÃO
    [HttpGet]
    public IActionResult Excluir(int id)
    {
        var projeto = _projetoService.ObterPorId(id);

        if (projeto is null)
            return NotFound();

        projeto.Professor = _professorService.ObterPorId(projeto.ProfessorId);

        return View(projeto);
    }

    // CONFIRMAR EXCLUSÃO
    [HttpPost]
    [ActionName("Excluir")]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmarExclusao(int id)
    {
        var removido = _projetoService.Remover(id);

        if (!removido)
            return NotFound();

        TempData["Mensagem"] = "Projeto excluído com sucesso!";

        return RedirectToAction(nameof(Index));
    }

    // Método auxiliar para popular os professores no select
    private List<SelectListItem> ObterListaProfessores()
    {
        return _professorService.ObterTodos()
            .Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nome
            }).ToList();
    }
}
