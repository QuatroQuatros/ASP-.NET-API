using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels;

public class CollectionDayViewModel
{
    [Required(ErrorMessage = "O ID da rua é obrigatório.")]
    public int StreetId { get; set; }
        
    [Required(ErrorMessage = "O ID do tipo de coleta é obrigatório.")]
    public int GarbageCollectionTypeId { get; set; }
    
    public DateTime? ScheduleDate { get; set; }
    
    public DateTime CollectionDate { get; set; }
    
    public CollectionStatus? Status { get; set; }
}