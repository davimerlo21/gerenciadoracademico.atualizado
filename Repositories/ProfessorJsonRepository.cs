using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorAcademico.Models;
using gerenciadoracademico.Repositories;

namespace GerenciadorAcademico.Repositories
{
    public class ProfessorJsonRepository : IProfessorRepository
    {
        private readonly JsonFileStore _store;
        private const string Arquivo = "professores.json";

        public ProfessorJsonRepository(JsonFileStore store)
        {
            _store = store;
        }

        // Alterado de ListarAsync para ObterTodosAsync para cumprir o contrato exigido
        public async Task<List<Professor>> ObterTodosAsync()
        {
            return await _store.LerAsync<List<Professor>>(Arquivo);
        }

        public async Task<Professor?> ObterPorIdAsync(int id)
        {
            var lista = await ObterTodosAsync();
            return lista.FirstOrDefault(p => p.Id == id);
        }
    }
}
