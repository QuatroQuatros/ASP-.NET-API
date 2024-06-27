using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels.Update;

public class RegionViewModelUpdate
{
    [Required(ErrorMessage = "O nome da região é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O id do estado é obrigatória.")]
    public int StateId { get; set; }
}