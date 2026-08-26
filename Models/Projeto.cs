namespace GerenciadorAcademico.Models;

public class Projeto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }

    // Relacionamento com Professor
    public int ProfessorId { get; set; } // Guarda o ID do Professor
    public Professor? Professor { get; set; } // Acessa os dados do Professor
}
