using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace GestaoDeResiduos.Models;

[Table("COLLECTION_DAY")]
public class CollectionDayModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }

    [Column("STREET_ID")]
    [ForeignKey("STREET")]
    public int StreetId { get; set; }
    
    [Column("GARBAGE_COLLECTION_TYPE_ID")]
    [ForeignKey("GARBAGE_COLLECTION_TYPES")]
    public int GarbageCollectionTypeId { get; set; }
    
    [Column("SCHEDULE_DATE")]
    public DateTime ScheduleDate { get; set; }

    [Column("COLLECTION_DATE")]
    public required DateTime CollectionDate { get; set; }

    [Column("STATUS")]
    public required CollectionStatus  Status { get; set; }
    
    public required StreetModel Street { get; set; }
    
    public required GarbageCollectionTypeModel GarbageCollectionType{ get; set; }
}


