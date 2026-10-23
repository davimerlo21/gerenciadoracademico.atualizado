using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorAcademico.Models;

namespace GerenciadorAcademico.Repositories
{
    public interface IProjetoRepository
    {
        // Métodos de consulta exigidos na aula
        Task<List<Projeto>> ObterTodosAsync();
        Task<Projeto?> ObterPorIdAsync(int id);

        // Métodos de escrita necessários para o Service compilar sem erros
        Task AdicionarAsync(Projeto projeto);
        Task AtualizarAsync(Projeto projeto);
        Task RemoverAsync(int id);
    }
}
