using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorAcademico.Models;
using GerenciadorAcademico.Data; // Ajustado para encontrar o JsonFileStore que está na pasta Data

namespace GerenciadorAcademico.Repositories
{
    public class ProjetoJsonRepository : IProjetoRepository
    {
        private readonly JsonFileStore _store;
        private const string Arquivo = "projetos.json";

        public ProjetoJsonRepository(JsonFileStore store)
        {
            _store = store;
        }

        public async Task<List<Projeto>> ObterTodosAsync()
        {
            return await _store.LerAsync<List<Projeto>>(Arquivo);
        }

        public async Task<Projeto?> ObterPorIdAsync(int id)
        {
            var lista = await ObterTodosAsync();
            return lista.FirstOrDefault(p => p.Id == id);
        }

        public async Task AdicionarAsync(Projeto projeto)
        {
            var lista = await ObterTodosAsync();

            projeto.Id = lista.Any() ? lista.Max(p => p.Id) + 1 : 1;

            var professorTemporario = projeto.Professor;
            projeto.Professor = null;

            lista.Add(projeto);
            await _store.SalvarSeguroAsync(Arquivo, lista);

            projeto.Professor = professorTemporario;
        }

        public async Task AtualizarAsync(Projeto projeto)
        {
            var lista = await ObterTodosAsync();
            var index = lista.FindIndex(p => p.Id == projeto.Id);

            if (index != -1)
            {
                lista[index].Titulo = projeto.Titulo;
                lista[index].Descricao = projeto.Descricao;
                lista[index].CargaHoraria = projeto.CargaHoraria;
                lista[index].ProfessorId = projeto.ProfessorId;
                lista[index].Professor = null;

                await _store.SalvarSeguroAsync(Arquivo, lista);
            }
        }

        public async Task RemoverAsync(int id)
        {
            var lista = await ObterTodosAsync();
            var item = lista.FirstOrDefault(p => p.Id == id);

            if (item != null)
            {
                lista.Remove(item);
                await _store.SalvarSeguroAsync(Arquivo, lista);
            }
        }
    }
}
