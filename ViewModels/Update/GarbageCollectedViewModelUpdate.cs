using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels.Update;

public class GarbageCollectedViewModelUpdate
{
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    public float Amount { get; set; }
}