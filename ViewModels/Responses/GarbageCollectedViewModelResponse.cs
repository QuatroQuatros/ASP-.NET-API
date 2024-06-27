namespace GestaoDeResiduos.Responses;

public class GarbageCollectedViewModelResponse
{

    public int Id { get; set; }
    
    public int CollectionDayId { get; set; }
    
    public float Amount { get; set; }
}