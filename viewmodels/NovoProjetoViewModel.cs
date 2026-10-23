using System.ComponentModel.DataAnnotations;

namespace GerenciadorAcademico.ViewModels;

public class NovoProjetoViewModel
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(150, ErrorMessage = "O título não pode exceder 150 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "A descrição não pode exceder 1000 caracteres.")]
    public string? Descricao { get; set; }

    [StringLength(80, ErrorMessage = "A área não pode exceder 80 caracteres.")]
    public string? Area { get; set; }

    [Required(ErrorMessage = "A carga horária é obrigatória.")]
    [Range(1, 1000, ErrorMessage = "A carga horária deve ser entre 1 e 1000 horas.")]
    public int CargaHoraria { get; set; }

    [Required(ErrorMessage = "Selecione um professor orientador.")]
    public int ProfessorId { get; set; }

    // ==========================================
    // PROPRIEDADES DA DATA E HORA QUE FALTAVAM:
    // ==========================================
    [Required(ErrorMessage = "A data de entrega é obrigatória.")]
    public string DataInput { get; set; } = string.Empty;

    [Required(ErrorMessage = "O horário de entrega é obrigatório.")]
    public string HoraInput { get; set; } = string.Empty;
}
