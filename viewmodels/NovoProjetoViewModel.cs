using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciadorAcademico.ViewModels;

public class NovoProjetoViewModel
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A carga horária é obrigatória.")]
    public int CargaHoraria { get; set; }

    // AS DUAS PROPRIEDADES QUE FALTAVAM PARA SUMIR O ERRO:
    public int ProfessorId { get; set; }
    public List<SelectListItem> Professores { get; set; } = [];
}
