using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels;

public class StreetViewModel
{
    [Required(ErrorMessage = "O nome da rua é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O id do bairro é obrigatório.")]
    public int DistrictId { get; set; }
}
