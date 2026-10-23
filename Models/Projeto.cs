using GerenciadorAcademico.Models;

namespace GerenciadorAcademico.Models;

public class Projeto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? Area { get; set; }
    public int CargaHoraria { get; set; }

    // ==========================================
    // PROPRIEDADE QUE ESTAVA FALTANDO:
    // ==========================================
    public DateTime DataEntrega { get; set; }

    // Relacionamento com Professor
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; } = null!;
}
