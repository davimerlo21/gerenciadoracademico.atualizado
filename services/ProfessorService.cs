using gerenciadoracademico.Models;
using GerenciadorAcademico.Models;

namespace gerenciadoracademico.services;

public class ProfessorService : IProfessorService
{
    private readonly List<Professor> _professores =
    [
        new Professor { Id = 1, Nome = "Ana Paula Souza", Email = "ana.souza@academia.com" },
        new Professor { Id = 2, Nome = "Carlos Mendes", Email = "carlos.mendes@academia.com" }
    ];

    public List<Professor> ObterTodos()
    {
        return _professores;
    }

    public Professor? ObterPorId(int id)
    {
        return _professores.FirstOrDefault(p => p.Id == id);
    }
}
