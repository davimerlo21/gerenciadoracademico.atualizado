using GerenciadorAcademico.Models;
using GerenciadorAcademico.ViewModels;

namespace GerenciadorAcademico.Services;

public interface IProjetoService
{
    List<Projeto> PesquisarPorTitulo(string? titulo);

    Projeto? ObterPorId(int id);

    List<Projeto> Ordenar(IEnumerable<Projeto> projetos, string? ordenarPor);

    void Adicionar(NovoProjetoViewModel model);

    bool Atualizar(EditarProjetoViewModel model);

    bool Remover(int id);
}