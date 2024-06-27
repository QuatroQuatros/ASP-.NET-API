using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels;

public class GarbageCollectedViewModel
{

    [Required(ErrorMessage = "O ID do agendamento é obrigatório.")]
    public int CollectionDayId { get; set; }
    
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    public float Amount { get; set; }
}