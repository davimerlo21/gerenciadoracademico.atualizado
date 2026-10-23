using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorAcademico.Models;

namespace GerenciadorAcademico.Repositories
{
    public interface IProfessorRepository
    {
        Task<List<Professor>> ObterTodosAsync();
        Task<Professor?> ObterPorIdAsync(int id);
    }
}
