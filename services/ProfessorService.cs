using GerenciadorAcademico.Models;

namespace GerenciadorAcademico.Services;

// ESTA INTERFACE ESTAVA FALTANDO E FOI ADICIONADA AQUI:
public interface IProfessorService
{
    Task<List<Professor>> ObterTodosAsync();
    Task<Professor?> ObterPorIdAsync(int id);
}

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
