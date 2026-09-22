using gerenciadoracademico.Models;
using GerenciadorAcademico.Models;

namespace gerenciadoracademico.services;

public interface IProfessorService
{
    Task<List<Professor>> ObterTodosAsync();
    Task<Professor?> ObterPorIdAsync(int id);
}