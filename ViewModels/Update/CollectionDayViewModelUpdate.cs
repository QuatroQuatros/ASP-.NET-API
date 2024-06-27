using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels.Update;

public class CollectionDayViewModelUpdate
{
    [Required(ErrorMessage = "O ID da rua é obrigatório.")]
    public int StreetId { get; set; }
        
    [Required(ErrorMessage = "O ID do tipo de coleta é obrigatório.")]
    public int GarbageCollectionTypeId { get; set; }
        
    [Required(ErrorMessage = "A data de agendamento é obrigatória.")]
    public DateTime ScheduleDate { get; set; }

    [Required(ErrorMessage = "A data de coleta é obrigatória.")]
    public DateTime CollectionDate { get; set; }

    [Required(ErrorMessage = "O status é obrigatório.")]
    public int Status { get; set; }
}