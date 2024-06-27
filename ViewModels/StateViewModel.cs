using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels;

public class StateViewModel
{
    [Required(ErrorMessage = "O nome do estado é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "A UF do estado é obrigatória.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF precisa ter 2 caracteres.")]
    public string? UF { get; set; }
}