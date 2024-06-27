namespace GestaoDeResiduos.Responses;

public class CollectionDayViewModelResponse
{
    public int Id { get; set; }
    public int StreetId { get; set; }
    public int GarbageCollectionTypeId { get; set; }
    public DateTime ScheduleDate { get; set; }
    public DateTime? CollectionDate { get; set; }
    public string Status { get; set; }
    public string StreetName { get; set; }
    public string GarbageCollectionTypeName { get; set; }
}