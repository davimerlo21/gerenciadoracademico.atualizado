using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorAcademico.Models;

namespace GerenciadorAcademico.Repositories
{
    public interface IProjetoRepository
    {
        Task<List<Projeto>> ObterTodosAsync();
        Task<Projeto?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Projeto projeto);
        Task AtualizarAsync(Projeto projeto);
        Task RemoverAsync(int id);
    }
}
