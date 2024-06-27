using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels;

public class DistrictViewModel
{
    [Required(ErrorMessage = "O nome do bairro é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O id da região é obrigatório.")]
    public int RegionId { get; set; }
}