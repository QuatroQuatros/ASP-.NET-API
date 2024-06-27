using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels;

public class RegionViewModel
{
    [Required(ErrorMessage = "O nome da região é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O id do estado é obrigatória.")]
    public int StateId { get; set; }
}