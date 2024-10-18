using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoDeResiduos.Models;

public class GarbageCollectedModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("COLLECTION_DAY_ID")]
    [ForeignKey("COLLECTION_DAY")]
    public int CollectionDayId { get; set; }
    
    [Column("AMOUNT")]
    public float Amount { get; set; }

    public CollectionDayModel? CollectionDay { get; set; }
}