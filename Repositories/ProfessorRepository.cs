using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerenciadorAcademico.Data;
using GerenciadorAcademico.Models;

namespace GerenciadorAcademico.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AcademicoContext _context;

        public ProfessorRepository(AcademicoContext context)
        {
            _context = context;
        }

        public async Task<List<Professor>> ObterTodosAsync()
        {
            return await _context.Professores
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Professor?> ObterPorIdAsync(int id)
        {
            return await _context.Professores
                .Include(p => p.Projetos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
