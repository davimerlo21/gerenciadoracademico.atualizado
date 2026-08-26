using GerenciadorAcademico.Models;

namespace GerenciadorAcademico.ViewModels;

public class ProjetosIndexViewModel
{
    public List<Projeto> projetos { get; set; } = new();

    public string? textopesquisa { get; set; }

    public int quantidadetotal { get; set; }

    public string? ordenarPor { get; set; }
}