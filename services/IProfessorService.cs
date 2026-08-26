using gerenciadoracademico.Models;
using GerenciadorAcademico.Models;

namespace gerenciadoracademico.services;

public interface IProfessorService
{
    List<Professor> ObterTodos();
    Professor? ObterPorId(int id);
}
