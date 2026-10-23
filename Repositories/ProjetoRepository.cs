using Microsoft.EntityFrameworkCore;
using GerenciadorAcademico.Data;
using GerenciadorAcademico.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorAcademico.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly AcademicoContext _context;

        public ProjetoRepository(AcademicoContext context)
        {
            _context = context;
        }

        public async Task<List<Projeto>> ObterTodosAsync()
        {
            return await _context.Projetos
                .Include(p => p.Professor)
                .ToListAsync();
        }

        public async Task<Projeto?> ObterPorIdAsync(int id)
        {
            return await _context.Projetos
                .Include(p => p.Professor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // ==========================================
        // MÉTODOS ADICIONADOS PARA CORRIGIR O ERRO:
        // ==========================================

        public async Task AdicionarAsync(Projeto projeto)
        {
            await _context.Projetos.AddAsync(projeto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Projeto projeto)
        {
            _context.Projetos.Update(projeto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto != null)
            {
                _context.Projetos.Remove(projeto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
