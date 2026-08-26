using GerenciadorAcademico.Models;
using GerenciadorAcademico.ViewModels;

namespace GerenciadorAcademico.Services;

public class ProjetoService : IProjetoService
{
    private readonly List<Projeto> _projetos =
    [
        new Projeto
        {
            Id = 1,
            Titulo = "Sistema de Biblioteca Escolar",
            Descricao = "Aplicação para controle de livros e empréstimos.",
            CargaHoraria = 80,
            ProfessorId = 1
        },

        new Projeto
        {
            Id = 2,
            Titulo = "Portal de Projetos Acadêmicos",
            Descricao = "Sistema para divulgação de projetos desenvolvidos pelos estudantes.",
            CargaHoraria = 60,
            ProfessorId = 2
        }
    ];

    public List<Projeto> Listar()
    {
        return _projetos;
    }

    public List<Projeto> PesquisarPorTitulo(string? titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return Listar();

        return _projetos
            .Where(projeto => projeto.Titulo.Contains(
                titulo,
                StringComparison.CurrentCultureIgnoreCase))
            .ToList();
    }

    public List<Projeto> Ordenar(IEnumerable<Projeto> projetos, string? ordenarPor)
    {
        return ordenarPor switch
        {
            "titulo" => projetos
                .OrderBy(projeto => projeto.Titulo)
                .ToList(),

            "titulo_desc" => projetos
                .OrderByDescending(projeto => projeto.Titulo)
                .ToList(),

            "carga" => projetos
                .OrderBy(projeto => projeto.CargaHoraria)
                .ToList(),

            "carga_desc" => projetos
                .OrderByDescending(projeto => projeto.CargaHoraria)
                .ToList(),

            _ => projetos.ToList()
        };
    }

    public Projeto? ObterPorId(int id)
    {
        return _projetos.FirstOrDefault(projeto => projeto.Id == id);
    }

    public void Adicionar(NovoProjetoViewModel model)
    {
        var novoProjeto = new Projeto
        {
            Id = GerarNovoId(),
            Titulo = model.Titulo,
            Descricao = model.Descricao,
            CargaHoraria = model.CargaHoraria,
            ProfessorId = model.ProfessorId
        };

        _projetos.Add(novoProjeto);
    }

    public bool Atualizar(EditarProjetoViewModel model)
    {
        var projeto = ObterPorId(model.Id);

        if (projeto is null)
            return false;

        projeto.Titulo = model.Titulo;
        projeto.Descricao = model.Descricao;
        projeto.CargaHoraria = model.CargaHoraria;
        projeto.ProfessorId = model.ProfessorId;

        return true;
    }

    public bool Remover(int id)
    {
        var projeto = ObterPorId(id);

        if (projeto is null)
            return false;

        _projetos.Remove(projeto);

        return true;
    }

    private int GerarNovoId()
    {
        return _projetos.Count == 0
            ? 1
            : _projetos.Max(projeto => projeto.Id) + 1;
    }
}
