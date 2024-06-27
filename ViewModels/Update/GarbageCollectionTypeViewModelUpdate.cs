using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels.Update;

public class GarbageCollectionTypeViewModelUpdate
{
    [Required(ErrorMessage = "O nome do tipo de coleta é obrigatório.")]
    public string? Name { get; set; }
}