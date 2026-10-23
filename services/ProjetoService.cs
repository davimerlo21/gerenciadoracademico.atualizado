using GerenciadorAcademico.Models;
using GerenciadorAcademico.Repositories;
using GerenciadorAcademico.ViewModels;

namespace GerenciadorAcademico.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _projetoRepository;
    private readonly IProfessorRepository _professorRepository;

    public ProjetoService(
        IProjetoRepository projetoRepository,
        IProfessorRepository professorRepository)
    {
        _projetoRepository = projetoRepository;
        _professorRepository = professorRepository;
    }

    public async Task<List<Professor>> ListarProfessoresAsync()
    {
        return await _professorRepository.ObterTodosAsync();
    }

    public async Task<List<Projeto>> ListarAsync()
    {
        return await _projetoRepository.ObterTodosAsync();
    }

    public async Task<List<Projeto>> PesquisarPorTituloAsync(string? titulo)
    {
        var projetos = await ListarAsync();

        if (string.IsNullOrWhiteSpace(titulo))
            return projetos;

        return projetos
            .Where(projeto => projeto.Titulo.Contains(
                titulo,
                StringComparison.CurrentCultureIgnoreCase))
            .ToList();
    }

    public List<Projeto> Ordenar(IEnumerable<Projeto> projetos, string? ordenarPor)
    {
        return ordenarPor switch
        {
            "titulo" => projetos.OrderBy(projeto => projeto.Titulo).ToList(),
            "titulo_desc" => projetos.OrderByDescending(projeto => projeto.Titulo).ToList(),
            "carga" => projetos.OrderBy(projeto => projeto.CargaHoraria).ToList(),
            "carga_desc" => projetos.OrderByDescending(projeto => projeto.CargaHoraria).ToList(),
            _ => projetos.ToList()
        };
    }

    public async Task<Projeto?> ObterPorIdAsync(int id)
    {
        return await _projetoRepository.ObterPorIdAsync(id);
    }

    public async Task AdicionarAsync(NovoProjetoViewModel model)
    {
        string dataHoraCompleta = $"{model.DataInput} {model.HoraInput}";

        if (!DateTime.TryParse(dataHoraCompleta, out DateTime dataConvertida))
        {
            dataConvertida = DateTime.Now;
        }

        var novoProjeto = new Projeto
        {
            Titulo = model.Titulo,
            Descricao = model.Descricao,
            Area = model.Area,
            CargaHoraria = model.CargaHoraria,
            ProfessorId = model.ProfessorId,
            DataEntrega = dataConvertida
        };

        await _projetoRepository.AdicionarAsync(novoProjeto);
    }

    public async Task<bool> AtualizarAsync(EditarProjetoViewModel model)
    {
        var projeto = await _projetoRepository.ObterPorIdAsync(model.Id);
        if (projeto is null)
            return false;

        projeto.Titulo = model.Titulo;
        projeto.Descricao = model.Descricao;
        projeto.CargaHoraria = model.CargaHoraria;
        projeto.ProfessorId = model.ProfessorId;

        await _projetoRepository.AtualizarAsync(projeto);
        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var projeto = await _projetoRepository.ObterPorIdAsync(id);
        if (projeto is null)
            return false;

        await _projetoRepository.RemoverAsync(id);
        return true;
    }
}
