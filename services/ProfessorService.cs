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

    public Task<List<Professor>> ObterTodosAsync()
    {
        return Task.FromResult(_professores);
    }

    public Task<Professor?> ObterPorIdAsync(int id)
    {
        var professor = _professores.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(professor);
    }
}