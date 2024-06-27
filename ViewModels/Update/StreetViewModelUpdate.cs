using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels.Update;

public class StreetViewModelUpdate
{
    [Required(ErrorMessage = "O nome da rua é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O id do bairro é obrigatório.")]
    public int DistrictId { get; set; }
}